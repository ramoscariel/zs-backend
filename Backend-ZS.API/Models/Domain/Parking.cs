namespace Backend_ZS.API.Models.Domain
{
    public class Parking : TransactionItem
    {
        public DateOnly Date { get; set; }
        public TimeOnly EntryTime { get; set; }
        public TimeOnly? ExitTime { get; set; }
    }
}
