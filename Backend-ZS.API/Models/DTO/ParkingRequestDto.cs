namespace Backend_ZS.API.Models.DTO
{
    public class ParkingRequestDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly EntryTime { get; set; }
        public TimeOnly? ExitTime { get; set; }
    }
}
