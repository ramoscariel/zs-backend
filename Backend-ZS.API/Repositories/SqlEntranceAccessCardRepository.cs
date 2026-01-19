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
            // Load associated AccessCard
            var card = await dbContext.AccessCards.FindAsync(entranceAccessCard.AccessCardId);
            if (card == null)
            {
                throw new InvalidOperationException("Associated AccessCard not found.");
            }

            // Prevent creation if no uses left
            if (card.Uses <= 0)
            {
                throw new InvalidOperationException("Associated AccessCard has no remaining uses.");
            }

            // Decrement uses for each entrance created
            card.Uses -= 1;

            await dbContext.EntranceAccessCards.AddAsync(entranceAccessCard);
            await dbContext.SaveChangesAsync();

            // Ensure navigation property is loaded before returning
            await dbContext.Entry(entranceAccessCard).Reference(e => e.AccessCard).LoadAsync();
            return entranceAccessCard;
        }

        public async Task<EntranceAccessCard?> UpdateAsync(Guid id, EntranceAccessCard entranceAccessCard)
        {
            var existing = await dbContext.EntranceAccessCards.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            // If AccessCardId changes, restore a use to the old card and consume one from the new card
            if (existing.AccessCardId != entranceAccessCard.AccessCardId)
            {
                var oldCard = await dbContext.AccessCards.FindAsync(existing.AccessCardId);
                var newCard = await dbContext.AccessCards.FindAsync(entranceAccessCard.AccessCardId);

                if (newCard == null)
                {
                    throw new InvalidOperationException("Associated new AccessCard not found.");
                }

                if (newCard.Uses <= 0)
                {
                    throw new InvalidOperationException("Associated new AccessCard has no remaining uses.");
                }

                // Return use to old card (if exists)
                if (oldCard != null)
                {
                    oldCard.Uses += 1;
                }

                // Consume a use from new card
                newCard.Uses -= 1;

                existing.AccessCardId = entranceAccessCard.AccessCardId;
            }

            // Update other properties
            existing.EntryTime = entranceAccessCard.EntryTime;
            existing.ExitTime = entranceAccessCard.ExitTime;
            existing.User = entranceAccessCard.User;

            await dbContext.SaveChangesAsync();

            // Ensure navigation property is loaded on the tracked entity
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
                card.Uses += 1;
            }

            dbContext.EntranceAccessCards.Remove(existing);
            await dbContext.SaveChangesAsync();

            return existing;
        }
    }
}
