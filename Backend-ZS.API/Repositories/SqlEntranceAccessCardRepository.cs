using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlEntranceAccessCardRepository : IEntranceAccessCardRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlEntranceAccessCardRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EntranceAccessCard>> GetAllAsync()
        {
            return await dbContext.EntranceAccessCards
                .Include(e => e.AccessCard)
                .ToListAsync();
        }

        public async Task<EntranceAccessCard?> GetByIdAsync(Guid id)
        {
            return await dbContext.EntranceAccessCards
                .Include(e => e.AccessCard)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EntranceAccessCard> AddAsync(EntranceAccessCard entranceAccessCard)
        {
            var card = await dbContext.AccessCards.FindAsync(entranceAccessCard.AccessCardId);
            if (card == null) throw new InvalidOperationException("Associated AccessCard not found.");

            var qty = entranceAccessCard.Qty <= 0 ? 1 : entranceAccessCard.Qty;

            if (card.Uses < qty)
                throw new InvalidOperationException($"Associated AccessCard has no remaining uses. Remaining: {card.Uses}");

            card.Uses -= qty;                 // ✅ antes era -1
            entranceAccessCard.Qty = qty;     // ✅ normaliza

            await dbContext.EntranceAccessCards.AddAsync(entranceAccessCard);
            await dbContext.SaveChangesAsync();

            await dbContext.Entry(entranceAccessCard).Reference(e => e.AccessCard).LoadAsync();
            return entranceAccessCard;
        }

        public async Task<EntranceAccessCard?> UpdateAsync(Guid id, EntranceAccessCard entranceAccessCard)
        {
            var existing = await dbContext.EntranceAccessCards.FindAsync(id);
            if (existing == null) return null;

            var newQty = entranceAccessCard.Qty <= 0 ? 1 : entranceAccessCard.Qty;

            // Si cambia de tarjeta
            if (existing.AccessCardId != entranceAccessCard.AccessCardId)
            {
                var oldCard = await dbContext.AccessCards.FindAsync(existing.AccessCardId);
                var newCard = await dbContext.AccessCards.FindAsync(entranceAccessCard.AccessCardId);

                if (newCard == null) throw new InvalidOperationException("Associated new AccessCard not found.");

                // devolver usos de la vieja tarjeta (por la qty anterior)
                if (oldCard != null) oldCard.Uses += Math.Max(1, existing.Qty);

                // consumir usos de la nueva tarjeta (por la nueva qty)
                if (newCard.Uses < newQty)
                    throw new InvalidOperationException($"Associated new AccessCard has no remaining uses. Remaining: {newCard.Uses}");

                newCard.Uses -= newQty;

                existing.AccessCardId = entranceAccessCard.AccessCardId;
                existing.Qty = newQty;
            }
            else
            {
                // misma tarjeta: ajustar solo la diferencia de qty
                var card = await dbContext.AccessCards.FindAsync(existing.AccessCardId);
                if (card == null) throw new InvalidOperationException("Associated AccessCard not found.");

                var oldQty = Math.Max(1, existing.Qty);
                var diff = newQty - oldQty;

                if (diff > 0)
                {
                    if (card.Uses < diff)
                        throw new InvalidOperationException($"Associated AccessCard has no remaining uses. Remaining: {card.Uses}");
                    card.Uses -= diff;
                }
                else if (diff < 0)
                {
                    card.Uses += (-diff);
                }

                existing.Qty = newQty;
            }

            existing.EntranceDate = entranceAccessCard.EntranceDate;
            existing.EntranceEntryTime = entranceAccessCard.EntranceEntryTime;
            existing.EntranceExitTime = entranceAccessCard.EntranceExitTime;

            await dbContext.SaveChangesAsync();
            await dbContext.Entry(existing).Reference(e => e.AccessCard).LoadAsync();
            return existing;
        }


        public async Task<EntranceAccessCard?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.EntranceAccessCards.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            // Restore a use to the associated access card when an entrance is removed
            var card = await dbContext.AccessCards.FindAsync(existing.AccessCardId);
            if (card != null)
            {
                card.Uses += Math.Max(1, existing.Qty);
            }

            dbContext.EntranceAccessCards.Remove(existing);
            await dbContext.SaveChangesAsync();

            return existing;
        }
    }
}
