namespace Backend_ZS.API.Models.Domain
{
    public class Parking : TransactionItem
    {
        public DateOnly ParkingDate { get; set; }
        public TimeOnly ParkingEntryTime { get; set; }
        public TimeOnly? ParkingExitTime { get; set; }
    }
}
