using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IAccessCardRepository
    {
        Task<List<AccessCard>> GetAllAsync();
        Task<AccessCard?> GetByIdAsync(Guid id);
        Task<AccessCard> AddAsync(AccessCard accessCard);
        Task<AccessCard?> UpdateAsync(Guid id, AccessCard accessCard);
        Task<AccessCard?> DeleteAsync(Guid id);
    }
}
