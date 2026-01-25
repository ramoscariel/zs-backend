using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public TransactionStatus Status { get; set; }

        public Guid ClientId { get; set; }
        public Guid CashBoxId { get; set; }

        public ClientDto Client { get; set; }
        public ICollection<TransactionItemDto> TransactionItems { get; set; }
        public ICollection<PaymentDto> Payments { get; set; }

        // ✅ NUEVO
        public ICollection<KeyDto> Keys { get; set; } = new List<KeyDto>();
    }
}
