using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(Guid id);
        Task<Payment> AddAsync(Payment payment);
        Task<Payment?> UpdateAsync(Guid id, Payment payment);
        Task<Payment?> DeleteAsync(Guid id);
    }
}
