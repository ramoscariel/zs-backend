namespace Backend_ZS.API.Models.DTO
{
    public class EntranceAccessCardDto
    {
        public Guid Id { get; set; }
        public Guid AccessCardId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
