// Controllers/ParkingsController.cs
using AutoMapper;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private readonly IParkingRepository parkingRepository;
        private readonly IMapper mapper;

        public ParkingsController(IParkingRepository parkingRepository, IMapper mapper)
        {
            this.parkingRepository = parkingRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parkings = await parkingRepository.GetAllAsync();
            var parkingsDto = mapper.Map<List<ParkingDto>>(parkings);
            return Ok(parkingsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var parkingDomainModel = await parkingRepository.GetByIdAsync(id);
            if (parkingDomainModel == null)
            {
                return NotFound();
            }

            var parkingDto = mapper.Map<ParkingDto>(parkingDomainModel);
            return Ok(parkingDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParkingRequestDto parkingRequestDto)
        {
            var parkingDomainModel = mapper.Map<Backend_ZS.API.Models.Domain.Parking>(parkingRequestDto);

            // ✅ FIX: asegurar TransactionType desde el controller también
            if (string.IsNullOrWhiteSpace(parkingDomainModel.TransactionType))
                parkingDomainModel.TransactionType = "Parking";

            parkingDomainModel = await parkingRepository.AddAsync(parkingDomainModel);

            var parkingDto = mapper.Map<ParkingDto>(parkingDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = parkingDto.Id }, parkingDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ParkingRequestDto parkingRequestDto)
        {
            var parkingDomainModel = mapper.Map<Backend_ZS.API.Models.Domain.Parking>(parkingRequestDto);

            // ✅ FIX: asegurar TransactionType
            if (string.IsNullOrWhiteSpace(parkingDomainModel.TransactionType))
                parkingDomainModel.TransactionType = "Parking";

            parkingDomainModel = await parkingRepository.UpdateAsync(id, parkingDomainModel);

            if (parkingDomainModel == null)
            {
                return NotFound();
            }

            var parkingDto = mapper.Map<ParkingDto>(parkingDomainModel);
            return Ok(parkingDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedParking = await parkingRepository.DeleteAsync(id);

            if (deletedParking == null)
            {
                return NotFound();
            }

            var parkingDto = mapper.Map<ParkingDto>(deletedParking);
            return Ok(parkingDto);
        }
    }
}
