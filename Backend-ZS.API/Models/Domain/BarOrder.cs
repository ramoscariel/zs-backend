namespace Backend_ZS.API.Models.Domain
{
    public class BarOrder : TransactionItem
    {
        // Navigation Property
        public ICollection<BarOrderDetail> Details { get; set; }
    }
}
