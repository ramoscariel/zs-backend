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

            // Calculate Total: if ExitTime is null -> 0, otherwise $1 per hour (round up)
            if (parking.ParkingExitTime == null)
            {
                parking.Total = 0;
            }
            else
            {
                var entry = parking.ParkingEntryTime;
                var exit = parking.ParkingExitTime.Value;

                var duration = exit.ToTimeSpan() - entry.ToTimeSpan();
                if (duration < TimeSpan.Zero)
                {
                    // handle spanning midnight
                    duration += TimeSpan.FromDays(1);
                }

                var hours = Math.Ceiling(duration.TotalHours);
                parking.Total = hours * 1.0;
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
            existingParking.ParkingDate = parking.ParkingDate;
            existingParking.ParkingEntryTime = parking.ParkingEntryTime;
            existingParking.ParkingExitTime = parking.ParkingExitTime;

            // Recalculate Total
            if (existingParking.ParkingExitTime == null)
            {
                existingParking.Total = 0;
            }
            else
            {
                var entry = existingParking.ParkingEntryTime;
                var exit = existingParking.ParkingExitTime.Value;

                var duration = exit.ToTimeSpan() - entry.ToTimeSpan();
                if (duration < TimeSpan.Zero)
                {
                    // handle spanning midnight
                    duration += TimeSpan.FromDays(1);
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
