using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IBarOrderRepository
    {
        Task<List<BarOrder>> GetAllAsync();
        Task<BarOrder?> GetByIdAsync(Guid id);
        Task<BarOrder> AddAsync(BarOrder barOrder);
        Task<BarOrder?> UpdateAsync(Guid id, BarOrder barOrder);
        Task<BarOrder?> DeleteAsync(Guid id);

        // BarOrderDetail Operations
        Task<BarOrderDetail?> GetDetailAsync(Guid orderId, Guid productId);
        Task<BarOrderDetail> AddDetailAsync(BarOrderDetail detail);
        Task<BarOrderDetail?> UpdateDetailAsync(BarOrderDetail detail);
        Task<BarOrderDetail?> DeleteDetailAsync(Guid orderId, Guid productId);
    }
}
