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
    public class EntranceTransactionsController : ControllerBase
    {
        private readonly IEntranceTransactionRepository entranceTransactionRepository;
        private readonly IMapper mapper;

        public EntranceTransactionsController(IEntranceTransactionRepository entranceTransactionRepository, IMapper mapper)
        {
            this.entranceTransactionRepository = entranceTransactionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await entranceTransactionRepository.GetAllAsync();
            var transactionsDto = mapper.Map<List<EntranceTransactionDto>>(transactions);
            return Ok(transactionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var transaction = await entranceTransactionRepository.GetByIdAsync(id);
            if (transaction == null) return NotFound();

            var transactionDto = mapper.Map<EntranceTransactionDto>(transaction);
            return Ok(transactionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EntranceTransactionRequestDto requestDto)
        {
            var domain = mapper.Map<EntranceTransaction>(requestDto);
            domain = await entranceTransactionRepository.AddAsync(domain);

            var dto = mapper.Map<EntranceTransactionDto>(domain);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EntranceTransactionRequestDto requestDto)
        {
            var domain = mapper.Map<EntranceTransaction>(requestDto);
            var updated = await entranceTransactionRepository.UpdateAsync(id, domain);

            if (updated == null) return NotFound();

            var dto = mapper.Map<EntranceTransactionDto>(updated);
            return Ok(dto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await entranceTransactionRepository.DeleteAsync(id);
            if (deleted == null) return NotFound();

            var dto = mapper.Map<EntranceTransactionDto>(deleted);
            return Ok(dto);
        }
    }
}
