
namespace Backend_ZS.API.Models.Domain
{
    public class EntranceAccessCard : IEntrance
    {
        public Guid Id { get; set; }
        public Guid AccessCardId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        // Navigation Properties
        public AccessCard AccessCard { get; set; }
    }
}
