using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlBarProductRepository : IBarProductRepository
    {
        private readonly ZsDbContext dbContext;

        public SqlBarProductRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<BarProduct>> GetAllAsync()
        {
            return await dbContext.BarProducts.ToListAsync();
        }

        public async Task<BarProduct?> GetByIdAsync(Guid id)
        {
            return await dbContext.BarProducts.FindAsync(id);
        }

        public async Task<BarProduct> AddAsync(BarProduct barProduct)
        {
            await dbContext.BarProducts.AddAsync(barProduct);
            await dbContext.SaveChangesAsync();
            return barProduct;
        }

        public async Task<BarProduct?> UpdateAsync(Guid id, BarProduct barProduct)
        {
            var existingProduct = await dbContext.BarProducts.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            // Update Properties
            existingProduct.Name = barProduct.Name;
            existingProduct.Qty = barProduct.Qty;
            existingProduct.UnitPrice = barProduct.UnitPrice;

            await dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<BarProduct?> DeleteAsync(Guid id)
        {
            var existingProduct = await dbContext.BarProducts.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            dbContext.BarProducts.Remove(existingProduct);
            await dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
