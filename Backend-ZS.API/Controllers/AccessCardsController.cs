using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessCardsController : ControllerBase
    {
        private readonly IAccessCardRepository accessCardRepository;
        private readonly IMapper mapper;
        public AccessCardsController(IAccessCardRepository accessCardRepository, IMapper mapper)
        {
            this.accessCardRepository = accessCardRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var accessCards = await accessCardRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var accessCardsDto = mapper.Map<List<AccessCardDto>>(accessCards);

            return Ok(accessCardsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var accessCardDomainModel = await accessCardRepository.GetByIdAsync(id);

            if (accessCardDomainModel == null)
            {
                return NotFound();
            }

            // 2 DTO
            var accessCardDto = mapper.Map<AccessCardDto>(accessCardDomainModel);

            return Ok(accessCardDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccessCardRequestDto accessCardRequestDto)
        {
            // Map RequestDto to Domain Model
            var accessCardDomainModel = mapper.Map<AccessCard>(accessCardRequestDto);

            // Create
            accessCardDomainModel = await accessCardRepository.AddAsync(accessCardDomainModel);

            // Map Domain Model to Dto
            var accessCardDto = mapper.Map<AccessCardDto>(accessCardDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = accessCardDto.Id }, accessCardDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AccessCardRequestDto accessCardRequestDto)
        {
            // Map RequestDto to Domain Model
            var accessCardDomainModel = mapper.Map<AccessCard>(accessCardRequestDto);

            accessCardDomainModel = await accessCardRepository.UpdateAsync(id, accessCardDomainModel);

            if (accessCardDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var accessCardDto = mapper.Map<AccessCardDto>(accessCardDomainModel);

            return Ok(accessCardDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedAccessCard = await accessCardRepository.DeleteAsync(id);

            if (deletedAccessCard == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var accessCardDto = mapper.Map<AccessCardDto>(deletedAccessCard);

            // return DTO
            return Ok(accessCardDto);
        }

    }
}
