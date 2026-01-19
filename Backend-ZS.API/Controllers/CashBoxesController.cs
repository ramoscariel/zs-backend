using AutoMapper;
using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashBoxesController : ControllerBase
    {
        private readonly ZsDbContext dbContext;
        private readonly IMapper mapper;

        public CashBoxesController(ZsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // GET: /api/CashBoxes/today?date=2026-01-11
        [HttpGet("today")]
        public async Task<IActionResult> GetToday([FromQuery] DateOnly? date = null)
        {
            var targetDate = date ?? DateOnly.FromDateTime(DateTime.Now);

            var cashBox = await dbContext.CashBoxes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => DateOnly.FromDateTime(x.OpenedAt) == targetDate);

            if (cashBox == null) return NotFound();

            return Ok(mapper.Map<CashBoxDto>(cashBox));
        }

        // POST: /api/CashBoxes/open
        [HttpPost("open")]
        public async Task<IActionResult> Open([FromBody] OpenCashBoxRequestDto req)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var existing = await dbContext.CashBoxes
                .FirstOrDefaultAsync(x => DateOnly.FromDateTime(x.OpenedAt) == today);

            if (existing != null)
            {
                if (existing.Status == CashBoxStatus.Open)
                    return Conflict($"Ya existe una caja ABIERTA para {today}.");

                return Conflict($"Ya existe una caja CERRADA para {today}.");
            }

            var cashBox = new CashBox
            {
                Id = Guid.NewGuid(),
                Status = CashBoxStatus.Open,
                OpenedAt = DateTime.UtcNow,
                OpeningBalance = req.OpeningBalance
            };

            dbContext.CashBoxes.Add(cashBox);
            await dbContext.SaveChangesAsync();

            return Ok(mapper.Map<CashBoxDto>(cashBox));
        }

        // POST: /api/CashBoxes/{id}/close
        [HttpPost("{id:guid}/close")]
        public async Task<IActionResult> Close([FromRoute] Guid id, [FromBody] CloseCashBoxRequestDto req)
        {
            var cashBox = await dbContext.CashBoxes.FirstOrDefaultAsync(x => x.Id == id);
            if (cashBox == null) return NotFound();

            if (cashBox.Status == CashBoxStatus.Closed)
                return Conflict("La caja ya está cerrada.");

            // Calcula total de pagos del día
            var totalPayments = await dbContext.Transactions
                .Where(t => t.CashBoxId == id)
                .SelectMany(t => t.Payments)
                .SumAsync(p => (double?)p.Total) ?? 0.0;

            cashBox.Status = CashBoxStatus.Closed;
            cashBox.ClosedAt = DateTime.UtcNow;

            // si mandan closing manual, úsalo; si no, calcula
            cashBox.ClosingBalance = req.ClosingBalance ?? (cashBox.OpeningBalance + totalPayments);

            await dbContext.SaveChangesAsync();

            return Ok(mapper.Map<CashBoxDto>(cashBox));
        }

        // GET: /api/CashBoxes/range?from=2026-01-01&to=2026-01-31
        [HttpGet("range")]
        public async Task<IActionResult> Range([FromQuery] DateOnly from, [FromQuery] DateOnly to)
        {
            var list = await dbContext.CashBoxes
                .AsNoTracking()
                .Where(x => DateOnly.FromDateTime(x.OpenedAt) >= from && DateOnly.FromDateTime(x.OpenedAt) <= to)
                .OrderByDescending(x => x.OpenedAt)
                .ToListAsync();

            return Ok(mapper.Map<List<CashBoxDto>>(list));
        }

        // GET: /api/CashBoxes/{id}/transactions
        [HttpGet("{id:guid}/transactions")]
        public async Task<IActionResult> GetTransactions([FromRoute] Guid id)
        {
            var exists = await dbContext.CashBoxes.AnyAsync(x => x.Id == id);
            if (!exists) return NotFound();

            var txs = await dbContext.Transactions
                .AsNoTracking()
                .Include(t => t.Client)
                .Include(t => t.TransactionItems)
                .Include(t => t.Payments)
                .Where(t => t.CashBoxId == id)
                .OrderByDescending(t => t.OpenedAt)
                .ToListAsync();

            return Ok(mapper.Map<List<TransactionDto>>(txs));
        }

        // GET: /api/CashBoxes/{id}/summary
        [HttpGet("{id:guid}/summary")]
        public async Task<IActionResult> Summary([FromRoute] Guid id)
        {
            var cashBox = await dbContext.CashBoxes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (cashBox == null) return NotFound();

            var txs = dbContext.Transactions.Where(t => t.CashBoxId == id);

            var totalCharges = await txs
                .SelectMany(t => t.TransactionItems)
                .SumAsync(i => (double?)i.Total) ?? 0.0;

            var totalPayments = await txs
                .SelectMany(t => t.Payments)
                .SumAsync(p => (double?)p.Total) ?? 0.0;

            var cashPayments = await txs
                .SelectMany(t => t.Payments)
                .Where(p => p.Type == PaymentType.Efectivo)
                .SumAsync(p => (double?)p.Total) ?? 0.0;

            var transferPayments = await txs
                .SelectMany(t => t.Payments)
                .Where(p => p.Type == PaymentType.Transferencia)
                .SumAsync(p => (double?)p.Total) ?? 0.0;

            var openCount = await txs.CountAsync(t => t.Status == TransactionStatus.Open);
            var closedCount = await txs.CountAsync(t => t.Status == TransactionStatus.Closed);

            return Ok(new
            {
                CashBox = mapper.Map<CashBoxDto>(cashBox),
                TotalCharges = totalCharges,
                TotalPayments = totalPayments,
                Cash = cashPayments,
                Transfer = transferPayments,
                OpenTransactions = openCount,
                ClosedTransactions = closedCount
            });
        }
    }
}
