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
            // ✅ Lockers necesita TODAS las llaves, sin incluir Transaction (evita ciclos/payload gigante)
            return await dbContext.Keys
                .AsNoTracking()
                .OrderBy(k => k.KeyCode)
                .ToListAsync();
        }

        public async Task<Key?> GetByIdAsync(Guid id)
        {
            return await dbContext.Keys
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<Key?> UpdateAsync(Guid id, Key key)
        {
            var existingKey = await dbContext.Keys.FirstOrDefaultAsync(k => k.Id == id);
            if (existingKey == null) return null;

            // ✅ Permitir asignar / liberar llave desde POS/Lockers
            // Si viene TransactionId:
            // - asigna: Available=false, LastAssignedAt=now
            // - libera: TransactionId=null, Available=true
            existingKey.TransactionId = key.TransactionId;

            if (key.TransactionId.HasValue)
            {
                existingKey.Available = false;
                existingKey.LastAssignedAt = DateTime.UtcNow;
            }
            else
            {
                existingKey.Available = true;
            }

            // Campos editables
            existingKey.Notes = key.Notes;

            await dbContext.SaveChangesAsync();

            return await dbContext.Keys
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);
        }
    }
}
