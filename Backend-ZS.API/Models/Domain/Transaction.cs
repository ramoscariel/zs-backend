using Microsoft.Identity.Client;

namespace Backend_ZS.API.Models.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid ClientId { get; set; }
        public Guid TransactionItemId { get; set; }
        public Guid? PaymentId { get; set; }

        // Navigation Properties
        public Client Client { get; set; }
        public TransactionItem TransactionItem { get; set; }
        public Payment Payment { get; set; }

    }
}
