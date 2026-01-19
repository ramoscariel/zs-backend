// Models/Domain/Parking.cs
namespace Backend_ZS.API.Models.Domain
{
    public class Parking : TransactionItem, IEntrance
    {
        // ✅ FIX: asegurar tipo desde el constructor (nunca NULL)
        public Parking()
        {
            TransactionType = "Parking";
        }

        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
