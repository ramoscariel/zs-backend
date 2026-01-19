using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlEntranceTransactionRepository : IEntranceTransactionRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlEntranceTransactionRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EntranceTransaction>> GetAllAsync()
        {
            return await dbContext.EntranceTransactions.ToListAsync();
        }

        public async Task<EntranceTransaction?> GetByIdAsync(Guid id)
        {
            return await dbContext.EntranceTransactions.FindAsync(id);
        }

        public async Task<EntranceTransaction> AddAsync(EntranceTransaction entranceTransaction)
        {
            // Calculate total per spec:
            // NumberAdults*7 + NumberChildren*4 + NumberSeniors*5 + NumberSeniors*5
            var total = (entranceTransaction.NumberAdults * 7)
                        + (entranceTransaction.NumberChildren * 4)
                        + (entranceTransaction.NumberSeniors * 5)
                        + (entranceTransaction.NumberDisabled * 5);

            entranceTransaction.Total = total;

            await dbContext.EntranceTransactions.AddAsync(entranceTransaction);
            await dbContext.SaveChangesAsync();
            return entranceTransaction;
        }

        public async Task<EntranceTransaction?> UpdateAsync(Guid id, EntranceTransaction entranceTransaction)
        {
            var existing = await dbContext.EntranceTransactions.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            // Update properties
            existing.EntryTime = entranceTransaction.EntryTime;
            existing.ExitTime = entranceTransaction.ExitTime;
            existing.NumberAdults = entranceTransaction.NumberAdults;
            existing.NumberChildren = entranceTransaction.NumberChildren;
            existing.NumberSeniors = entranceTransaction.NumberSeniors;
            existing.NumberDisabled = entranceTransaction.NumberDisabled;

            // Recalculate total (same formula as Add)
            var total = (existing.NumberAdults * 7)
                        + (existing.NumberChildren * 4)
                        + (existing.NumberSeniors * 5)
                        + (existing.NumberDisabled * 5);

            existing.Total = total;

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<EntranceTransaction?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.EntranceTransactions.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            dbContext.EntranceTransactions.Remove(existing);
            await dbContext.SaveChangesAsync();

            return existing;
        }
    }
}
