using AutoMapper;
using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ZsDbContext dbContext;
        private readonly IMapper mapper;

        public TransactionsController(ITransactionRepository transactionRepository, ZsDbContext dbContext, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? cashBoxId = null)
        {
            if (cashBoxId.HasValue)
            {
                var transactions = await dbContext.Transactions
                    .Include(t => t.Client)
                    .Include(t => t.TransactionItems)
                    .Include(t => t.Payments)
                    .Where(t => t.CashBoxId == cashBoxId.Value)
                    .OrderByDescending(t => t.OpenedAt)
                    .ToListAsync();

                return Ok(mapper.Map<List<TransactionDto>>(transactions));
            }

            var all = await transactionRepository.GetAllAsync();
            return Ok(mapper.Map<List<TransactionDto>>(all));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var transactionDomainModel = await transactionRepository.GetByIdAsync(id);
            if (transactionDomainModel == null) return NotFound();

            return Ok(mapper.Map<TransactionDto>(transactionDomainModel));
        }

        // ✅ Crear cuenta: requiere caja abierta hoy
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRequestDto transactionRequestDto)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var cashBox = await dbContext.CashBoxes
                .FirstOrDefaultAsync(x => DateOnly.FromDateTime(x.OpenedAt) == today && x.Status == CashBoxStatus.Open);

            if (cashBox == null)
                return Conflict("No hay caja abierta hoy. Abre caja antes de crear una cuenta.");

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                ClientId = transactionRequestDto.ClientId,
                CashBoxId = cashBox.Id,
                OpenedAt = DateTime.UtcNow,
                Status = TransactionStatus.Open
            };

            transaction = await transactionRepository.AddAsync(transaction);

            var dto = mapper.Map<TransactionDto>(transaction);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // ✅ Cerrar cuenta
        [HttpPost("{id:guid}/close")]
        public async Task<IActionResult> Close([FromRoute] Guid id)
        {
            var tx = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
            if (tx == null) return NotFound();

            if (tx.Status == TransactionStatus.Closed)
                return Conflict("La cuenta ya está cerrada.");

            tx.Status = TransactionStatus.Closed;
            tx.ClosedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            // devolver con includes
            var full = await transactionRepository.GetByIdAsync(id);
            return Ok(mapper.Map<TransactionDto>(full));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TransactionRequestDto transactionRequestDto)
        {
            // Solo permito cambiar clientId. NO toco cashBoxId.
            var existing = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            existing.ClientId = transactionRequestDto.ClientId;
            await dbContext.SaveChangesAsync();

            var full = await transactionRepository.GetByIdAsync(id);
            return Ok(mapper.Map<TransactionDto>(full));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await transactionRepository.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return Ok(mapper.Map<TransactionDto>(deleted));
        }
    }
}
