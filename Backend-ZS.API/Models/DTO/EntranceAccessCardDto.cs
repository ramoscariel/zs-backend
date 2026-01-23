namespace Backend_ZS.API.Models.DTO
{
    public class EntranceAccessCardDto
    {
        public Guid Id { get; set; }
        public Guid AccessCardId { get; set; }
        public DateOnly EntranceDate { get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }

        public int Qty { get; set; } // ✅ NUEVO
    }
}
