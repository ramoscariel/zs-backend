using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlKeyRepository : IKeyRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlKeyRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Key>> GetAllAsync()
        {
            return await dbContext.Keys
                .Include(k => k.LastAssignedClient)
                .ToListAsync();
        }

        public async Task<Key?> UpdateAsync(Guid id, Key key)
        {
            var existingKey = await dbContext.Keys.FindAsync(id);
            if (existingKey == null)
            {
                return null;
            }

            // Update Props
            existingKey.LastAssignedTo = key.LastAssignedTo;
            existingKey.Available = key.Available;
            existingKey.Notes = key.Notes;

            // Load Nav Props
            await dbContext.Entry(existingKey).Reference(k => k.LastAssignedClient).LoadAsync();

            await dbContext.SaveChangesAsync();
            return existingKey;
        }
    }
}
