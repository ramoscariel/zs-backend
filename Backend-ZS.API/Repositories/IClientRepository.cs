using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(Guid id);
        Task<Client> AddAsync(Client client);
        Task<Client?> UpdateAsync(Guid id, Client client);
        Task<Client?> DeleteAsync(Guid id);
    }
}
