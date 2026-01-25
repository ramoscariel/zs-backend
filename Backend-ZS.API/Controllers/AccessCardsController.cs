using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AccessCardsController : ControllerBase
    {
        private readonly IAccessCardRepository accessCardRepository;
        private readonly IMapper mapper;

        public AccessCardsController(
            IAccessCardRepository accessCardRepository,
            IMapper mapper
        )
        {
            this.accessCardRepository = accessCardRepository;
            this.mapper = mapper;
        }

        // GET: api/AccessCards
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accessCards = await accessCardRepository.GetAllAsync();
            var accessCardsDto = mapper.Map<List<AccessCardDto>>(accessCards);
            return Ok(accessCardsDto);
        }

        // GET: api/AccessCards/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var accessCardDomainModel = await accessCardRepository.GetByIdAsync(id);
            if (accessCardDomainModel == null) return NotFound();

            var accessCardDto = mapper.Map<AccessCardDto>(accessCardDomainModel);
            return Ok(accessCardDto);
        }

        // POST: api/AccessCards
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccessCardRequestDto accessCardRequestDto)
        {
            if (accessCardRequestDto == null)
                return BadRequest("Body requerido.");

            // ✅ NORMALIZACIÓN: si TransactionId viene vacío, no lo uses (evita FK)
            if (accessCardRequestDto.TransactionId == Guid.Empty)
                accessCardRequestDto.TransactionId = Guid.Empty; // dejamos vacío; el mapper lo convertirá a null si lo configuraste

            // Si Uses viene 0, por defecto 10
            if (accessCardRequestDto.Uses <= 0)
                accessCardRequestDto.Uses = 10;

            var accessCardDomainModel = mapper.Map<AccessCard>(accessCardRequestDto);

            // ✅ Garantiza valores seguros
            accessCardDomainModel.TransactionType = "AccessCard";

            // ✅ EVITA FK: si no estás creando dentro de una Transaction, deja TransactionId nulo
            // Nota: para esto TransactionItem.TransactionId debe ser Guid? (nullable) + migración
            if (accessCardRequestDto.TransactionId == Guid.Empty)
                accessCardDomainModel.TransactionId = null;

            accessCardDomainModel = await accessCardRepository.AddAsync(accessCardDomainModel);

            var accessCardDto = mapper.Map<AccessCardDto>(accessCardDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = accessCardDto.Id }, accessCardDto);
        }

        // PUT: api/AccessCards/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AccessCardRequestDto accessCardRequestDto)
        {
            if (accessCardRequestDto == null)
                return BadRequest("Body requerido.");

            if (accessCardRequestDto.Uses < 0)
                return BadRequest("Uses no puede ser negativo.");

            var accessCardDomainModel = mapper.Map<AccessCard>(accessCardRequestDto);

            // seguridad: nunca cambies TransactionId por PUT aquí (se usa para relacionar)
            // si tu mapper lo está seteando, lo anulamos:
            accessCardDomainModel.TransactionId = null;
            accessCardDomainModel.TransactionType = "AccessCard";

            var updated = await accessCardRepository.UpdateAsync(id, accessCardDomainModel);
            if (updated == null) return NotFound();

            var accessCardDto = mapper.Map<AccessCardDto>(updated);
            return Ok(accessCardDto);
        }

        // DELETE: api/AccessCards/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedAccessCard = await accessCardRepository.DeleteAsync(id);
            if (deletedAccessCard == null) return NotFound();

            var accessCardDto = mapper.Map<AccessCardDto>(deletedAccessCard);
            return Ok(accessCardDto);
        }
    }
}
