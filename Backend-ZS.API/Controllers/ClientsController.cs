using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        public ClientsController(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        /* -------------------- HELPERS -------------------- */

        private static string? NullIfWhiteSpace(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

        private static void Normalize(ClientRequestDto dto)
        {
            dto.Name = (dto.Name ?? "").Trim();
            dto.DocumentNumber = NullIfWhiteSpace(dto.DocumentNumber);
            dto.Email = NullIfWhiteSpace(dto.Email);
            dto.Address = NullIfWhiteSpace(dto.Address);
            dto.Number = NullIfWhiteSpace(dto.Number);
        }

        /* -------------------- GET -------------------- */

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await clientRepository.GetAllAsync();
            return Ok(mapper.Map<List<ClientDto>>(clients));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(mapper.Map<ClientDto>(client));
        }

        /* -------------------- CREATE -------------------- */

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientRequestDto dto)
        {
            Normalize(dto);

            var client = mapper.Map<Client>(dto);
            client = await clientRepository.AddAsync(client);

            return CreatedAtAction(nameof(GetById),
                new { id = client.Id },
                mapper.Map<ClientDto>(client));
        }

        /* -------------------- UPDATE -------------------- */

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ClientRequestDto dto)
        {
            Normalize(dto);

            var client = mapper.Map<Client>(dto);
            client = await clientRepository.UpdateAsync(id, client);

            if (client == null) return NotFound();

            return Ok(mapper.Map<ClientDto>(client));
        }

        /* -------------------- DELETE -------------------- */

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await clientRepository.DeleteAsync(id);
            if (client == null) return NotFound();
            return Ok(mapper.Map<ClientDto>(client));
        }
    }
}
