namespace Backend_ZS.API.Models.DTO
{
    public class EntranceAccessCardRequestDto
    {
        public Guid AccessCardId { get; set; }
        public DateOnly EntranceDate { get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }
    }
}
