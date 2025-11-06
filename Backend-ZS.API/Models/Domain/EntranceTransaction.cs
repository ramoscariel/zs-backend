

namespace Backend_ZS.API.Models.Domain
{
    public class EntranceTransaction : TransactionItem, IEntrance
    {
        public DateOnly Date { get; set; }
        public TimeOnly EntryTime { get; set; }
        public TimeOnly ExitTime { get; set; }
        public int NumberPersons {  get; set; }
        public int NumberAdults { get; set; }
        public int NumberChildren { get; set; }
        public int NumberSeniors { get; set; }
        public int NumberDisabled { get; set; }
    }
}
