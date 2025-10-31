using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IBarProductRepository
    {
        Task<List<BarProduct>> GetAllAsync();
        Task<BarProduct?> GetByIdAsync(Guid id);
        Task<BarProduct> AddAsync(BarProduct barProduct);
        Task<BarProduct?> UpdateAsync(Guid id, BarProduct barProduct);
        Task<BarProduct?> DeleteAsync(Guid id);
    }
}
