using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(Guid id, Transaction transaction);
        Task<Transaction?> DeleteAsync(Guid id);
    }
}
