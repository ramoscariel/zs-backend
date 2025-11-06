namespace Backend_ZS.API.Models.Domain
{
    public abstract class TransactionItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Total { get; set; }
    }
}
