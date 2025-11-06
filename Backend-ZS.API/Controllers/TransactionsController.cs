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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;
        public TransactionsController(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var transactions = await transactionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var transactionsDto = mapper.Map<List<TransactionDto>>(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var transactionDomainModel = await transactionRepository.GetByIdAsync(id);

            if (transactionDomainModel == null)
            {
                return NotFound();
            }

            // 2 DTO
            var transactionDto = mapper.Map<TransactionDto>(transactionDomainModel);

            return Ok(transactionDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRequestDto transactionRequestDto)
        {
            // Map RequestDto to Domain Model
            var transactionDomainModel = mapper.Map<Transaction>(transactionRequestDto);

            // Create
            transactionDomainModel = await transactionRepository.AddAsync(transactionDomainModel);

            // Map Domain Model to Dto
            var transactionDto = mapper.Map<TransactionDto>(transactionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = transactionDto.Id }, transactionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TransactionRequestDto transactionRequestDto)
        {
            // Map RequestDto to Domain Model
            var transactionDomainModel = mapper.Map<Transaction>(transactionRequestDto);

            transactionDomainModel = await transactionRepository.UpdateAsync(id, transactionDomainModel);

            if (transactionDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var transactionDto = mapper.Map<TransactionDto>(transactionDomainModel);

            return Ok(transactionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedtransaction = await transactionRepository.DeleteAsync(id);

            if (deletedtransaction == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var transactionDto = mapper.Map<TransactionDto>(deletedtransaction);

            // return DTO to transaction
            return Ok(transactionDto);
        }
    }
}
