namespace Backend_ZS.API.Models.DTO
{
    public class ParkingRequestDto : TransactionItemRequestDto
    {
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
