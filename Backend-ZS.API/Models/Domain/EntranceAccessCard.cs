
namespace Backend_ZS.API.Models.Domain
{
    public class EntranceAccessCard : IEntrance
    {
        public Guid Id { get; set; }
        public DateOnly EntranceDate {  get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }
    }
}
