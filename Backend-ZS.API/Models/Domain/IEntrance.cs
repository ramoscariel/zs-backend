namespace Backend_ZS.API.Models.Domain
{
    public interface IEntrance
    {
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
