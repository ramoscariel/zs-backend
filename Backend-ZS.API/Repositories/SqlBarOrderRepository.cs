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
                .Include("Details")
                .ToListAsync();
        }

        public async Task<BarOrder?> GetByIdAsync(Guid id)
        {
            return await dbContext.BarOrders
                .Include("Details")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BarOrder> AddAsync(BarOrder barOrder)
        {
            await dbContext.BarOrders.AddAsync(barOrder);
            await dbContext.SaveChangesAsync();
            return barOrder;
        }
        public async Task<BarOrder?> UpdateAsync(Guid id, BarOrder barOrder)
        {
            var existingBarOrder = await dbContext.BarOrders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingBarOrder == null)
            {
                return null;
            }

            // Update Properties

            existingBarOrder.Total = barOrder.Total;

            await dbContext.SaveChangesAsync();
            return existingBarOrder;
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

        // BarOrderDetail Operations
        public async Task<BarOrderDetail?> GetDetailAsync(Guid orderId, Guid productId)
        {
            return await dbContext.Set<BarOrderDetail>()
                .Include(d => d.BarOrder)
                .Include(d => d.BarProduct)
                .FirstOrDefaultAsync(d => d.BarOrderId == orderId && d.BarProductId == productId);
        }

        public async Task<BarOrderDetail> AddDetailAsync(BarOrderDetail detail)
        {
            await dbContext.Set<BarOrderDetail>().AddAsync(detail);
            await dbContext.SaveChangesAsync();

            // Ensure navigation properties are loaded before returning
            await dbContext.Entry(detail).Reference(d => d.BarOrder).LoadAsync();
            await dbContext.Entry(detail).Reference(d => d.BarProduct).LoadAsync();

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

            // Ensure navigation properties are loaded before returning
            await dbContext.Entry(detail).Reference(d => d.BarOrder).LoadAsync();
            await dbContext.Entry(detail).Reference(d => d.BarProduct).LoadAsync();

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<BarOrderDetail?> DeleteDetailAsync(Guid orderId, Guid productId)
        {
            var existing = await dbContext.Set<BarOrderDetail>()
                .FirstOrDefaultAsync(d => d.BarOrderId == orderId && d.BarProductId == productId);

            if (existing == null)
                return null;

            // Ensure navigation properties are loaded before returning
            await dbContext.Entry(existing).Reference(d => d.BarOrder).LoadAsync();
            await dbContext.Entry(existing).Reference(d => d.BarProduct).LoadAsync();

            dbContext.Set<BarOrderDetail>().Remove(existing);
            await dbContext.SaveChangesAsync();

            return existing;
        }
    }
}
