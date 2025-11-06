using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TransactionItemId { get; set; }
        public Guid PaymentId { get; set; }

        // Navigation Properties
        public TransactionItem TransactionItem { get; set; }
        public Payment Payment { get; set; }
    }
}
