// Models/Domain/Parking.cs
namespace Backend_ZS.API.Models.Domain
{
    public class Parking : TransactionItem
    {
        // ✅ FIX: asegurar tipo desde el constructor (nunca NULL)
        public Parking()
        {
            TransactionType = "Parking";
        }

        public DateOnly ParkingDate { get; set; }
        public TimeOnly ParkingEntryTime { get; set; }
        public TimeOnly? ParkingExitTime { get; set; }
    }
}
