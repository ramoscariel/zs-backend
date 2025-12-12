using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ClientId { get; set; }
        public Guid? PaymentId { get; set; }

        // Navigation Properties
        public ClientDto Client { get; set; }
        public PaymentDto Payment { get; set; }
        public ICollection<TransactionItemDto> TransactionItems { get; set; }

    }
}
