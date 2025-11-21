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
    public class EntranceAccessCardsController : ControllerBase
    {
        private readonly IEntranceAccessCardRepository entranceRepo;
        private readonly IMapper mapper;

        public EntranceAccessCardsController(IEntranceAccessCardRepository entranceRepo, IMapper mapper)
        {
            this.entranceRepo = entranceRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var domain = await entranceRepo.GetAllAsync();
            var dto = mapper.Map<List<EntranceAccessCardDto>>(domain);
            return Ok(dto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var domain = await entranceRepo.GetByIdAsync(id);
            if (domain == null) return NotFound();
            return Ok(mapper.Map<EntranceAccessCardDto>(domain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EntranceAccessCardRequestDto request)
        {
            var domain = mapper.Map<EntranceAccessCard>(request);

            try
            {
                domain = await entranceRepo.AddAsync(domain);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            var dto = mapper.Map<EntranceAccessCardDto>(domain);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EntranceAccessCardRequestDto request)
        {
            // Map RequestDto to Domain Model
            var domainToUpdate = mapper.Map<EntranceAccessCard>(request);

            // Attempt update
            EntranceAccessCard? updatedDomain;
            try
            {
                updatedDomain = await entranceRepo.UpdateAsync(id, domainToUpdate);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            if (updatedDomain == null)
            {
                return NotFound();
            }

            var updatedDto = mapper.Map<EntranceAccessCardDto>(updatedDomain);
            return Ok(updatedDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedDomain = await entranceRepo.DeleteAsync(id);

            if (deletedDomain == null)
            {
                return NotFound();
            }

            var deletedDto = mapper.Map<EntranceAccessCardDto>(deletedDomain);
            return Ok(deletedDto);
        }
    }
}
