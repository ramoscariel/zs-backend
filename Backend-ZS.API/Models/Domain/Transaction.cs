namespace Backend_ZS.API.Models.Domain
{
    public class Transaction : ISession
    {
        public Guid Id { get; set; }

        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public TransactionStatus Status { get; set; } = TransactionStatus.Open;

        public Guid ClientId { get; set; }

        public Guid CashBoxId { get; set; }
        public CashBox CashBox { get; set; }

        public Client Client { get; set; }
        public ICollection<TransactionItem> TransactionItems { get; set; } = new List<TransactionItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // ✅ NUEVO: llaves asignadas a la cuenta
        public ICollection<Key> Keys { get; set; } = new List<Key>();
    }

    public enum TransactionStatus
    {
        Open = 0,
        Closed = 1
    }
}
