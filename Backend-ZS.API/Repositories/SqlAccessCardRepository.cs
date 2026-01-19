using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlAccessCardRepository : IAccessCardRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlAccessCardRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<AccessCard>> GetAllAsync()
        {
            return await dbContext.AccessCards.ToListAsync();
        }

        public async Task<AccessCard?> GetByIdAsync(Guid id)
        {
            return await dbContext.AccessCards.FindAsync(id);
        }

        public async Task<AccessCard> AddAsync(AccessCard accessCard)
        {
            await dbContext.AccessCards.AddAsync(accessCard);
            await dbContext.SaveChangesAsync();
            return accessCard;
        }
        public async Task<AccessCard?> UpdateAsync(Guid id, AccessCard accessCard)
        {
            var existingCard = await dbContext.AccessCards.FindAsync(id);
            if (existingCard == null)
            {
                return null;
            }

            // Update Properties
            existingCard.Total = accessCard.Total;
            existingCard.Uses = accessCard.Uses;

            await dbContext.SaveChangesAsync();
            return existingCard;
        }

        public async Task<AccessCard?> DeleteAsync(Guid id)
        {
            var existingCard = await dbContext.AccessCards.FindAsync(id);
            if (existingCard == null)
            {
                return null;
            }

            dbContext.AccessCards.Remove(existingCard);
            await dbContext.SaveChangesAsync();

            return existingCard;
        }
    }
}
