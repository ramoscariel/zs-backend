using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IEntranceTransactionRepository
    {
        Task<List<EntranceTransaction>> GetAllAsync();
        Task<EntranceTransaction?> GetByIdAsync(Guid id);
        Task<EntranceTransaction> AddAsync(EntranceTransaction entranceTransaction);
        Task<EntranceTransaction?> UpdateAsync(Guid id, EntranceTransaction entranceTransaction);
        Task<EntranceTransaction?> DeleteAsync(Guid id);
    }
}
