namespace Backend_ZS.API.Models.Domain
{
    public abstract class TransactionItem
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
    }
}
