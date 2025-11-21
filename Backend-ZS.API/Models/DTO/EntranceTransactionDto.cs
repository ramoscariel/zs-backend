namespace Backend_ZS.API.Models.DTO
{
    public class EntranceTransactionDto
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateOnly EntranceDate { get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }
        public int NumberAdults { get; set; }
        public int NumberChildren { get; set; }
        public int NumberSeniors { get; set; }
        public int NumberDisabled { get; set; }
    }
}
