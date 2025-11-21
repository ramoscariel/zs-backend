using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Services
{
    public class SqlBarOrderService : IBarOrderService
    {
        private readonly ZsDbContext _db;

        public SqlBarOrderService(ZsDbContext db)
        {
            _db = db;
        }

        public async Task<BarOrderDetail> AddDetailAsync(BarOrderDetail detail)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();

            var product = await _db.BarProducts.FirstOrDefaultAsync(p => p.Id == detail.BarProductId)
                          ?? throw new InvalidOperationException("Product not found");

            if (product.Qty < detail.Qty)
                throw new InvalidOperationException("Insufficient stock");

            product.Qty -= detail.Qty;

            await _db.BarOrderDetails.AddAsync(detail);
            await _db.SaveChangesAsync();

            await UpdateOrderTotalAsync(detail.BarOrderId);

            await tx.CommitAsync();

            await _db.Entry(detail).Reference(d => d.BarProduct).LoadAsync();
            return detail;
        }

        public async Task<BarOrderDetail?> UpdateDetailAsync(BarOrderDetail updated)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();

            var existing = await _db.BarOrderDetails
                .FirstOrDefaultAsync(d => d.BarOrderId == updated.BarOrderId && d.BarProductId == updated.BarProductId);

            if (existing == null)
                return null;

            var product = await _db.BarProducts.FirstOrDefaultAsync(p => p.Id == existing.BarProductId)
                          ?? throw new InvalidOperationException("Product not found");

            var delta = updated.Qty - existing.Qty;
            if (delta > 0 && product.Qty < delta)
                throw new InvalidOperationException("Insufficient stock for requested increase");

            product.Qty -= delta;

            existing.Qty = updated.Qty;
            existing.UnitPrice = updated.UnitPrice;

            await _db.SaveChangesAsync();

            await UpdateOrderTotalAsync(existing.BarOrderId);

            await tx.CommitAsync();

            await _db.Entry(existing).Reference(d => d.BarProduct).LoadAsync();
            return existing;
        }

        public async Task<BarOrderDetail?> DeleteDetailAsync(Guid orderId, Guid productId)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();

            var existing = await _db.BarOrderDetails
                .FirstOrDefaultAsync(d => d.BarOrderId == orderId && d.BarProductId == productId);

            if (existing == null)
                return null;

            var product = await _db.BarProducts.FirstOrDefaultAsync(p => p.Id == existing.BarProductId)
                          ?? throw new InvalidOperationException("Product not found");

            await _db.Entry(existing).Reference(d => d.BarProduct).LoadAsync();

            product.Qty += existing.Qty;

            _db.BarOrderDetails.Remove(existing);
            await _db.SaveChangesAsync();

            await UpdateOrderTotalAsync(orderId);

            await tx.CommitAsync();

            return existing;
        }

        private async Task UpdateOrderTotalAsync(Guid orderId)
        {
            var total = await _db.BarOrderDetails
                .Where(d => d.BarOrderId == orderId)
                .Select(d => d.UnitPrice * d.Qty)
                .SumAsync();

            var order = await _db.BarOrders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.Total = total;
                await _db.SaveChangesAsync();
            }
        }
    }
}