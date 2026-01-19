namespace Backend_ZS.API.Models.Domain
{
    public class CashBox : ISession
    {
        public Guid Id { get; set; }

        public CashBoxStatus Status { get; set; } = CashBoxStatus.Open;

        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public double OpeningBalance { get; set; }
        public double? ClosingBalance { get; set; }

        // Nav
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public enum CashBoxStatus
    {
        Open = 0,
        Closed = 1
    }
}
