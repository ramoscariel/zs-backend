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
            // Cargar con hijos que bloquean el delete
            var existingTransaction = await dbContext.Transactions
                .Include(t => t.TransactionItems)
                .Include(t => t.Payments)
                .Include(t => t.Keys)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTransaction == null)
                return null;

            // 1) Soltar llaves (por seguridad; igual tienes SetNull en FK)
            foreach (var k in existingTransaction.Keys)
                k.TransactionId = null;

            // 2) Borrar Payments (si mantienes Restrict en Payments, esto es obligatorio)
            if (existingTransaction.Payments?.Count > 0)
                dbContext.Payments.RemoveRange(existingTransaction.Payments);

            // 3) Borrar TransactionItems (TPH: AccessCard/BarOrder/EntranceTransaction/Parking)
            if (existingTransaction.TransactionItems?.Count > 0)
                dbContext.Set<TransactionItem>().RemoveRange(existingTransaction.TransactionItems);

            // 4) Borrar Transaction
            dbContext.Transactions.Remove(existingTransaction);
            await dbContext.SaveChangesAsync();

            return existingTransaction;
        }

    }
}
