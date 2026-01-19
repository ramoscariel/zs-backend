namespace Backend_ZS.API.Models.Domain
{
    public class EntranceTransaction : TransactionItem, IEntrance
    {
        public EntranceTransaction()
        {
            TransactionType = "EntranceTransaction";
        }

        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int NumberAdults { get; set; }
        public int NumberChildren { get; set; }
        public int NumberSeniors { get; set; }
        public int NumberDisabled { get; set; }
    }
}
