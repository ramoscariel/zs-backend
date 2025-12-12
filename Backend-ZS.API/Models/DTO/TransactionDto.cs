using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid ClientId { get; set; }

        // Navigation Properties
        public ClientDto Client { get; set; }
        public ICollection<TransactionItemDto> TransactionItems { get; set; }
        public ICollection<PaymentDto> Payments { get; set; }

    }
}
