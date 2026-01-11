namespace Backend_ZS.API.Models.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }

        // Cuenta POS: entrada/salida
        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public TransactionStatus Status { get; set; } = TransactionStatus.Open;

        public Guid ClientId { get; set; }

        // ✅ Caja diaria obligatoria
        public Guid CashBoxId { get; set; }
        public CashBox CashBox { get; set; }

        // Navigation Properties
        public Client Client { get; set; }
        public ICollection<TransactionItem> TransactionItems { get; set; } = new List<TransactionItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

    public enum TransactionStatus
    {
        Open = 0,
        Closed = 1
    }
}
