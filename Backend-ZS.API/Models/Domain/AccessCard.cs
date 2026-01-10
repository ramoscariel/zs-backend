namespace Backend_ZS.API.Models.Domain
{
    public class AccessCard : TransactionItem
    {
        public string HolderName { get; set; } = "";
        public int Uses { get; set; } = 10;
    }
}
