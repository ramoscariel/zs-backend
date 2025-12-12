namespace Backend_ZS.API.Models.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public double Total { get; set; }
        public PaymentType Type { get; set; }
        public Guid TransactionId { get; set; }

        // Nav Props
        public Transaction Transaction { get; set; }
    }

    public enum PaymentType
    {
        Efectivo = 0,
        Transferencia = 1
    }
}
