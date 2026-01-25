using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlTransactionRepository : ITransactionRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlTransactionRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Transaction>> GetAllAsync()
        {
            return await dbContext.Transactions
                .Include(t => t.CashBox)    
                .Include(t => t.Client)
                .Include(t => t.TransactionItems)
                .Include(t => t.Payments)
                .Include(t => t.Keys) // ✅
                .ToListAsync();
        }
        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await dbContext.Transactions
                .Include(t => t.CashBox)
                .Include(t => t.Client)
                .Include(t => t.TransactionItems)
                .Include(t => t.Payments)
                .Include(t => t.Keys) // ✅
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            return transaction;
        }
        public async Task<Transaction?> UpdateAsync(Guid id, Transaction transaction)
        {
            var existingTransaction = await dbContext.Transactions
               .Include(t => t.TransactionItems)
               .FirstOrDefaultAsync(x => x.Id == id);
            if (existingTransaction == null)
            {
                return null;
            }

            // Update Properties
            existingTransaction.ClientId = transaction.ClientId;

            await dbContext.SaveChangesAsync();
            return existingTransaction;
        }

        public async Task<Transaction?> DeleteAsync(Guid id)
        {
            var existingTransaction = await dbContext.Transactions.FindAsync(id);
            if (existingTransaction == null)
            {
                return null;
            }

            dbContext.Transactions.Remove(existingTransaction);
            await dbContext.SaveChangesAsync();

            return existingTransaction;
        }
    }
}
