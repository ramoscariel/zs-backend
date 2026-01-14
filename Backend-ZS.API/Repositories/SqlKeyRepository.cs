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

        public async Task<Key?> GetByIdAsync(Guid id)
        {
            return await dbContext.Keys
                .Include(k => k.LastAssignedClient)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<Key?> UpdateAsync(Guid id, Key key)
        {
            var existingKey = await dbContext.Keys.FirstOrDefaultAsync(k => k.Id == id);
            if (existingKey == null) return null;

            existingKey.LastAssignedTo = key.LastAssignedTo;
            existingKey.LastAssignedAt = key.LastAssignedAt; // ✅ NUEVO
            existingKey.Available = key.Available;
            existingKey.Notes = key.Notes;

            await dbContext.SaveChangesAsync();

            return await dbContext.Keys
                .Include(k => k.LastAssignedClient)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

    }
}
