using AutoMapper;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Http;
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
            // Get Domain Models
            var parkings = await parkingRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var parkingsDto = mapper.Map<List<ParkingDto>>(parkings);

            return Ok(parkingsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var parkingDomainModel = await parkingRepository.GetByIdAsync(id);

            if (parkingDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var parkingDto = mapper.Map<ParkingDto>(parkingDomainModel);

            return Ok(parkingDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParkingRequestDto parkingRequestDto)
        {
            // Map RequestDto to Domain Model
            var parkingDomainModel = mapper.Map<Models.Domain.Parking>(parkingRequestDto);

            // Create
            parkingDomainModel = await parkingRepository.AddAsync(parkingDomainModel);

            // Map Domain Model to Dto
            var parkingDto = mapper.Map<ParkingDto>(parkingDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = parkingDto.Id }, parkingDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ParkingRequestDto parkingRequestDto)
        {
            // Map RequestDto to Domain Model
            var parkingDomainModel = mapper.Map<Models.Domain.Parking>(parkingRequestDto);

            parkingDomainModel = await parkingRepository.UpdateAsync(id, parkingDomainModel);

            if (parkingDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var parkingDto = mapper.Map<ParkingDto>(parkingDomainModel);

            return Ok(parkingDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedParking = await parkingRepository.DeleteAsync(id);

            if (deletedParking == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var parkingDto = mapper.Map<ParkingDto>(deletedParking);

            // return DTO to client
            return Ok(parkingDto);
        }
    }
}
