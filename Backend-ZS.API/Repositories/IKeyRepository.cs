using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IKeyRepository
    { 
        Task<List<Key>> GetAllAsync();
        Task<Key?> UpdateAsync(Guid id, Key key);
    }
}
