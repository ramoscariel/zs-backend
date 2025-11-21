namespace Backend_ZS.API.Models.DTO
{
    public class ParkingRequestDto
    {
        public DateOnly ParkingDate { get; set; }
        public TimeOnly ParkingEntryTime { get; set; }
        public TimeOnly? ParkingExitTime { get; set; }
    }
}
