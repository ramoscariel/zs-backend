using AutoMapper;
using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
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

        private static TimeZoneInfo GetEcuadorTz()
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"); }
            catch { return TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"); }
        }

        private static DateOnly EcuadorToday(TimeZoneInfo tz)
        {
            var localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            return DateOnly.FromDateTime(localNow);
        }

        private static (DateTime utcStart, DateTime utcEnd) UtcRangeForLocalDate(DateOnly localDate, TimeZoneInfo tz)
        {
            var localStart = DateTime.SpecifyKind(localDate.ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);
            var localEnd = DateTime.SpecifyKind(localDate.AddDays(1).ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);

            var utcStart = TimeZoneInfo.ConvertTimeToUtc(localStart, tz);
            var utcEnd = TimeZoneInfo.ConvertTimeToUtc(localEnd, tz);

            return (utcStart, utcEnd);
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

        // ✅ Crear cuenta: usa CashBoxId si viene y es válido; si no, usa la caja ABIERTA de HOY (Ecuador)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tz = GetEcuadorTz();
            var todayLocal = EcuadorToday(tz);
            var (utcStart, utcEnd) = UtcRangeForLocalDate(todayLocal, tz);

            // 1) Intentar con el CashBoxId que manda el front
            CashBox? cashBox = null;

            if (dto.CashBoxId != Guid.Empty)
            {
                cashBox = await dbContext.CashBoxes.FirstOrDefaultAsync(x =>
                    x.Id == dto.CashBoxId &&
                    x.Status == CashBoxStatus.Open &&
                    x.OpenedAt >= utcStart && x.OpenedAt < utcEnd
                );
            }

            // 2) Fallback: caja abierta de HOY (Ecuador) aunque el front mande un id malo/viejo
            if (cashBox == null)
            {
                cashBox = await dbContext.CashBoxes
                    .OrderByDescending(x => x.OpenedAt)
                    .FirstOrDefaultAsync(x =>
                        x.Status == CashBoxStatus.Open &&
                        x.OpenedAt >= utcStart && x.OpenedAt < utcEnd
                    );
            }

            if (cashBox == null)
                return Conflict("No hay caja ABIERTA válida para hoy (Ecuador). Abre caja antes de crear una cuenta.");

            // 3) Evitar duplicado: cliente con cuenta abierta en esa caja
            var hasOpen = await dbContext.Transactions.AnyAsync(t =>
                t.ClientId == dto.ClientId &&
                t.CashBoxId == cashBox.Id &&
                t.Status == TransactionStatus.Open
            );

            if (hasOpen)
                return Conflict("El cliente ya tiene una cuenta ABIERTA en la caja actual.");

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
                CashBoxId = cashBox.Id,
                OpenedAt = DateTime.UtcNow,
                Status = TransactionStatus.Open
            };

            await transactionRepository.AddAsync(transaction);

            var outDto = mapper.Map<TransactionDto>(transaction);
            return CreatedAtAction(nameof(GetById), new { id = outDto.Id }, outDto);
        }


        [HttpPost("{id:guid}/close")]
        public async Task<IActionResult> Close([FromRoute] Guid id)
        {
            var tx = await dbContext.Transactions
                .Include(t => t.TransactionItems)
                .Include(t => t.Payments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tx == null) return NotFound();

            if (tx.Status == TransactionStatus.Closed)
                return Conflict("La cuenta ya está cerrada.");

            var totalCharges = tx.TransactionItems?.Sum(i => (decimal)i.Total) ?? 0;
            var totalPayments = tx.Payments?.Sum(p => (decimal)p.Total) ?? 0;
            var pendingBalance = totalCharges - totalPayments;

            if (pendingBalance > 0)
                return Conflict($"No se puede cerrar. Saldo pendiente: ${pendingBalance:F2}");

            tx.Status = TransactionStatus.Closed;
            tx.ClosedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            var full = await transactionRepository.GetByIdAsync(id);
            return Ok(mapper.Map<TransactionDto>(full));
        }

        [HttpGet("{id:guid}/detail")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id)
        {
            var tx = await dbContext.Transactions
                .Include(t => t.Client)
                .Include(t => t.TransactionItems)
                .Include(t => t.Payments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tx == null) return NotFound();

            var entrances = await dbContext.EntranceTransactions.Where(e => e.TransactionId == id).ToListAsync();
            var parkings = await dbContext.Parkings.Where(p => p.TransactionId == id).ToListAsync();

            var barOrders = await dbContext.BarOrders
                .Include(b => b.Details)
                    .ThenInclude(d => d.BarProduct)
                .Where(b => b.TransactionId == id)
                .ToListAsync();

            var accessCards = await dbContext.AccessCards.Where(a => a.TransactionId == id).ToListAsync();
            var keys = await dbContext.Keys.Where(k => k.TransactionId == id).ToListAsync();

            var totalCharges = tx.TransactionItems?.Sum(i => (decimal)i.Total) ?? 0;
            var totalPayments = tx.Payments?.Sum(p => (decimal)p.Total) ?? 0;

            var detail = new TransactionDetailDto
            {
                Transaction = mapper.Map<TransactionDto>(tx),
                Entrances = mapper.Map<List<EntranceTransactionDto>>(entrances),
                Parkings = mapper.Map<List<ParkingDto>>(parkings),
                BarOrders = mapper.Map<List<BarOrderDto>>(barOrders),
                AccessCards = mapper.Map<List<AccessCardDto>>(accessCards),
                Keys = mapper.Map<List<KeyDto>>(keys),
                TotalCharges = totalCharges,
                TotalPayments = totalPayments,
                PendingBalance = totalCharges - totalPayments
            };

            return Ok(detail);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TransactionRequestDto dto)
        {
            var existing = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            existing.ClientId = dto.ClientId;
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
