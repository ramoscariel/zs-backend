namespace Backend_ZS.API.Models.DTO
{
    public class ParkingDto : TransactionItemDto
    {
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
