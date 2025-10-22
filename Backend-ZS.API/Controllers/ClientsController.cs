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
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        public ClientsController(IClientRepository walkRepository, IMapper mapper)
        {
            this.clientRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var clients = await clientRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var clientsDto = mapper.Map<List<ClientDto>>(clients);

            return Ok(clientsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var clientDomainModel = await clientRepository.GetByIdAsync(id);

            if (clientDomainModel == null) {
                return NotFound();
            }

            // 2 DTO
            var clientDto = mapper.Map<ClientDto>(clientDomainModel);

            return Ok(clientDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientRequestDto clientRequestDto)
        {
            // Map RequestDto to Domain Model
            var clientDomainModel = mapper.Map<Client>(clientRequestDto);

            // Create
            clientDomainModel = await clientRepository.AddAsync(clientDomainModel);

            // Map Domain Model to Dto
            var clientDto = mapper.Map<ClientDto>(clientDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = clientDto.Id }, clientDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] ClientRequestDto clientRequestDto)
        {
            // Map RequestDto to Domain Model
            var clientDomainModel = mapper.Map<Client>(clientRequestDto);

            clientDomainModel = await clientRepository.UpdateAsync(id, clientDomainModel);

            if (clientDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var clientDto = mapper.Map<ClientDto>(clientDomainModel);

            return Ok(clientDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedClient = await clientRepository.DeleteAsync(id);

            if (deletedClient == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var clientDto = mapper.Map<ClientDto>(deletedClient);

            // return DTO to client
            return Ok(clientDto);
        }
    }

}
