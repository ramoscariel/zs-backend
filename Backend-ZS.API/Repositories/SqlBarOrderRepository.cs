using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlBarOrderRepository : IBarOrderRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlBarOrderRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<BarOrder>> GetAllAsync()
        {
            return await dbContext.BarOrders
                .Include(o => o.Details)
                    .ThenInclude(d => d.BarProduct)
                .ToListAsync();
        }

        public async Task<BarOrder?> GetByIdAsync(Guid id)
        {
            return await dbContext.BarOrders
                .Include("Details")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BarOrder> AddAsync()
        {
            // Create a new BarOrder with default values
            var barOrder = new BarOrder
            {
                Total = 0,
                Details = new List<BarOrderDetail>()
            };

            await dbContext.BarOrders.AddAsync(barOrder);
            await dbContext.SaveChangesAsync();
            return barOrder;
        }

        public async Task<BarOrder?> DeleteAsync(Guid id)
        {
            var existingBarOrder = await dbContext.BarOrders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingBarOrder == null)
            {
                return null;
            }

            dbContext.BarOrders.Remove(existingBarOrder);
            await dbContext.SaveChangesAsync();

            return existingBarOrder;
        }

        ///
        // BarOrderDetail Operations
        ///
        public async Task<BarOrderDetail?> GetDetailAsync(Guid orderId, Guid productId)
        {
            return await dbContext.Set<BarOrderDetail>()
                .Include(d => d.BarProduct)
                .FirstOrDefaultAsync(d => d.BarOrderId == orderId && d.BarProductId == productId);
        }

        public async Task<BarOrderDetail> AddDetailAsync(BarOrderDetail detail)
        {
            await dbContext.Set<BarOrderDetail>().AddAsync(detail);
            await dbContext.SaveChangesAsync();

            // Ensure navigation properties are loaded before returning
            await dbContext.Entry(detail).Reference(d => d.BarProduct).LoadAsync();

            // Update the parent order total
            await UpdateOrderTotalAsync(detail.BarOrderId);

            return detail;
        }

        public async Task<BarOrderDetail?> UpdateDetailAsync(BarOrderDetail detail)
        {
            var existing = await dbContext.Set<BarOrderDetail>()
                .FirstOrDefaultAsync(d => d.BarOrderId == detail.BarOrderId && d.BarProductId == detail.BarProductId);

            if (existing == null)
                return null;

            existing.UnitPrice = detail.UnitPrice;
            existing.Qty = detail.Qty;

            await dbContext.SaveChangesAsync();

            // Ensure navigation properties are loaded on the tracked entity before returning
            await dbContext.Entry(existing).Reference(d => d.BarProduct).LoadAsync();

            // Update the parent order total
            await UpdateOrderTotalAsync(existing.BarOrderId);

            return existing;
        }

        public async Task<BarOrderDetail?> DeleteDetailAsync(Guid orderId, Guid productId)
        {
            var existing = await dbContext.Set<BarOrderDetail>()
                .FirstOrDefaultAsync(d => d.BarOrderId == orderId && d.BarProductId == productId);

            if (existing == null)
                return null;

            // Ensure navigation properties are loaded before returning
            await dbContext.Entry(existing).Reference(d => d.BarProduct).LoadAsync();

            dbContext.Set<BarOrderDetail>().Remove(existing);
            await dbContext.SaveChangesAsync();

            // Update the parent order total
            await UpdateOrderTotalAsync(orderId);

            return existing;
        }

        // Helper: recalculates and persists the total for a BarOrder based on its details
        private async Task UpdateOrderTotalAsync(Guid orderId)
        {
            // Calculate sum(unitPrice * qty) on DB side
            var total = await dbContext.BarOrderDetails
                .Where(d => d.BarOrderId == orderId)
                .Select(d => d.UnitPrice * d.Qty)
                .SumAsync();

            var order = await dbContext.BarOrders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.Total = total;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
