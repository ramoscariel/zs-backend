namespace Backend_ZS.API.Models.Domain
{
    public class EntranceAccessCard : IEntrance
    {
        public Guid Id { get; set; }
        public Guid AccessCardId { get; set; }
        public DateOnly EntranceDate { get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }

        public int Qty { get; set; } = 1; // ✅ NUEVO

        // Navigation Properties
        public AccessCard AccessCard { get; set; }
    }
}
