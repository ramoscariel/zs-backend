using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Services
{
    public interface IBarOrderService
    {
        Task<BarOrderDetail> AddDetailAsync(BarOrderDetail detail);
        Task<BarOrderDetail?> UpdateDetailAsync(BarOrderDetail detail);
        Task<BarOrderDetail?> DeleteDetailAsync(Guid orderId, Guid productId);
    }
}
