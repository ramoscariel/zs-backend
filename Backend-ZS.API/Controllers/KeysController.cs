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
    public class KeysController : ControllerBase
    {
        private readonly IKeyRepository keyRepository;
        private readonly IMapper mapper;

        public KeysController(IKeyRepository keyRepository, IMapper mapper)
        {
            this.keyRepository = keyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var keys = await keyRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var keysDto = mapper.Map<List<KeyDto>>(keys);

            return Ok(keysDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var key = await keyRepository.GetByIdAsync(id);
            if (key == null)
            {
                return NotFound();
            }

            var keyDto = mapper.Map<KeyDto>(key);
            return Ok(keyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] KeyRequestDto keyRequestDto)
        {
            // Map RequestDto to Domain Model
            var keyDomainModel = mapper.Map<Key>(keyRequestDto);

            keyDomainModel = await keyRepository.UpdateAsync(id, keyDomainModel);

            if (keyDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var keyDto = mapper.Map<KeyDto>(keyDomainModel);
            return Ok(keyDto);
        }
    }
}
