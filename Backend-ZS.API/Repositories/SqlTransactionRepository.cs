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
            return await dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await dbContext.Transactions.FindAsync(id);
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            return transaction;
        }
        public async Task<Transaction?> UpdateAsync(Guid id, Transaction transaction)
        {
            var existingTransaction = await dbContext.Transactions.FindAsync(id);
            if (existingTransaction == null)
            {
                return null;
            }

            // Update Properties
            existingTransaction.CreatedAt = transaction.CreatedAt;
            existingTransaction.TransactionItemId = transaction.TransactionItemId;
            existingTransaction.PaymentId = transaction.PaymentId;

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
