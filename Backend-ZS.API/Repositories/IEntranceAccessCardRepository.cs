using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IEntranceAccessCardRepository
    {
        Task<List<EntranceAccessCard>> GetAllAsync();
        Task<EntranceAccessCard?> GetByIdAsync(Guid id);
        Task<EntranceAccessCard> AddAsync(EntranceAccessCard entranceAccessCard);
        Task<EntranceAccessCard?> UpdateAsync(Guid id, EntranceAccessCard entranceAccessCard);
        Task<EntranceAccessCard?> DeleteAsync(Guid id);
    }
}
