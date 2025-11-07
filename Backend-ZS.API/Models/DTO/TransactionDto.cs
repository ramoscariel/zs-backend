using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ClientDto Client { get; set; }
        public TransactionItemDto TransactionItem { get; set; }
        public PaymentDto Payment { get; set; }
        
    }
}
