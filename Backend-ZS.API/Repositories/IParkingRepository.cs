using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IParkingRepository
    {
        Task<List<Parking>> GetAllAsync();
        Task<Parking?> GetByIdAsync(Guid id);
        Task<Parking> AddAsync(Parking parking);
        Task<Parking?> UpdateAsync(Guid id, Parking parking);
        Task<Parking?> DeleteAsync(Guid id);
    }
}
