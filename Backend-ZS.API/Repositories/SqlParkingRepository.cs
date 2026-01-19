// Repositories/SqlParkingRepository.cs
using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlParkingRepository : IParkingRepository
    {
        private readonly ZsDbContext dbContext;

        public SqlParkingRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Parking>> GetAllAsync()
        {
            return await dbContext.Parkings.ToListAsync();
        }

        public async Task<Parking?> GetByIdAsync(Guid id)
        {
            return await dbContext.Parkings.FindAsync(id);
        }

        public async Task<Parking> AddAsync(Parking parking)
        {
            // ✅ FIX: TransactionType nunca puede ser NULL/empty
            if (string.IsNullOrWhiteSpace(parking.TransactionType))
                parking.TransactionType = "Parking";

            // Calculate Total: if ExitTime is null -> 0, otherwise $0.50 per hour (round up)
            if (parking.ExitTime == null)
            {
                parking.Total = 0;
            }
            else
            {
                var duration = parking.ExitTime.Value - parking.EntryTime;
                if (duration < TimeSpan.Zero)
                {
                    duration = TimeSpan.Zero;
                }

                var hours = Math.Ceiling(duration.TotalHours);
                parking.Total = hours * 0.5;
            }

            await dbContext.Parkings.AddAsync(parking);
            await dbContext.SaveChangesAsync();

            return parking;
        }

        public async Task<Parking?> UpdateAsync(Guid id, Parking parking)
        {
            var existingParking = await dbContext.Parkings.FindAsync(id);
            if (existingParking == null)
            {
                return null;
            }

            // ✅ FIX: asegurar tipo también al actualizar
            if (string.IsNullOrWhiteSpace(existingParking.TransactionType))
                existingParking.TransactionType = "Parking";

            // Update Properties
            existingParking.EntryTime = parking.EntryTime;
            existingParking.ExitTime = parking.ExitTime;

            // Recalculate Total
            if (existingParking.ExitTime == null)
            {
                existingParking.Total = 0;
            }
            else
            {
                var duration = existingParking.ExitTime.Value - existingParking.EntryTime;
                if (duration < TimeSpan.Zero)
                {
                    duration = TimeSpan.Zero;
                }

                var hours = Math.Ceiling(duration.TotalHours);
                // Tarifa: $0.50 por hora o fracción
                existingParking.Total = hours * 0.5;
            }

            await dbContext.SaveChangesAsync();
            return existingParking;
        }

        public async Task<Parking?> DeleteAsync(Guid id)
        {
            // Trae lo mínimo sin tracking (evita conflictos por tracking/concurrency antiguo)
            var existing = await dbContext.Parkings
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existing == null) return null;

            try
            {
                // Attach + Remove asegura que EF arme el DELETE por PK
                dbContext.Parkings.Attach(existing);
                dbContext.Parkings.Remove(existing);

                await dbContext.SaveChangesAsync();
                return existing;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Si no afectó filas: para DELETE lo tratamos como "ya no existe"
                return null;
            }
        }

    }
}
