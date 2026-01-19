namespace Backend_ZS.API.Models.Domain
{
    public class BarOrder : TransactionItem
    {
        public BarOrder()
        {
            TransactionType = "BarOrder";
        }

        // Navigation Property
        public ICollection<BarOrderDetail> Details { get; set; }
    }
}
