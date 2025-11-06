using System.Transactions;

namespace Backend_ZS.API.Models.DTO
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Total { get; set; }
    }
}
