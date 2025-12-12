using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Total { get; set; }
        public PaymentType Type { get; set; }
        public Guid TransactionId { get; set; }

        // Nav Props
        public TransactionDto Transaction { get; set; }
    }
}
