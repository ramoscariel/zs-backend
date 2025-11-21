namespace Backend_ZS.API.Models.DTO
{
    public class ParkingDto
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateOnly ParkingDate { get; set; }
        public TimeOnly ParkingEntryTime { get; set; }
        public TimeOnly? ParkingExitTime { get; set; }
    }
}
