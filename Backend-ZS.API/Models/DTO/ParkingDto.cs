namespace Backend_ZS.API.Models.DTO
{
    public class ParkingDto
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly EntryTime { get; set; }
        public TimeOnly? ExitTime { get; set; }
    }
}
