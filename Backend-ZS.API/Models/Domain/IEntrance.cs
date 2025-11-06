namespace Backend_ZS.API.Models.Domain
{
    public interface IEntrance
    {
        public DateOnly Date { get; set; }
        public TimeOnly EntryTime { get; set; }
        public TimeOnly ExitTime { get; set; }
    }
}
