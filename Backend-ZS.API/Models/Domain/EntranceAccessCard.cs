
namespace Backend_ZS.API.Models.Domain
{
    public class EntranceAccessCard : IEntrance
    {
        public Guid Id { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly EntryTime { get; set; }
        public TimeOnly ExitTime { get; set; }
    }
}
