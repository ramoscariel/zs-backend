namespace Backend_ZS.API.Models.DTO
{
    public class EntranceAccessCardRequestDto
    {
        public Guid AccessCardId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? User { get; set; }
    }
}
