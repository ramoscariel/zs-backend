using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlPaymentRepository : IPaymentRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlPaymentRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Payment>> GetAllAsync()
        {
            return await dbContext.Payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await dbContext.Payments.FindAsync(id);
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();
            return payment;
        }
        public async Task<Payment?> UpdateAsync(Guid id, Payment payment)
        {
            var existingpayment = await dbContext.Payments.FindAsync(id);
            if (existingpayment == null)
            {
                return null;
            }

            // Update Properties
            existingpayment.Total = payment.Total;
            existingpayment.Type = payment.Type;

            await dbContext.SaveChangesAsync();
            return existingpayment;
        }

        public async Task<Payment?> DeleteAsync(Guid id)
        {
            var existingpayment = await dbContext.Payments.FindAsync(id);
            if (existingpayment == null)
            {
                return null;
            }

            dbContext.Payments.Remove(existingpayment);
            await dbContext.SaveChangesAsync();

            return existingpayment;
        }
    }
}
