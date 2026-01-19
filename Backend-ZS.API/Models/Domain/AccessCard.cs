namespace Backend_ZS.API.Models.Domain
{
    public class AccessCard : TransactionItem
    {
        public AccessCard()
        {
            TransactionType = "AccessCard";
        }

        public int Uses { get; set; } = 10;
    }
}
