namespace Backend_ZS.API.Models.Domain
{
    public interface IEntrance
    {
        public DateOnly EntranceDate { get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }
    }
}
