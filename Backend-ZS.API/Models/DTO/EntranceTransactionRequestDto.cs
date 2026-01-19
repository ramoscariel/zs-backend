namespace Backend_ZS.API.Models.DTO
{
    public class EntranceTransactionRequestDto : TransactionItemRequestDto
    {
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int NumberAdults { get; set; }
        public int NumberChildren { get; set; }
        public int NumberSeniors { get; set; }
        public int NumberDisabled { get; set; }
    }
}
