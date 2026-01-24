namespace Backend_ZS.API.Models.Domain
{
    public class EntranceAccessCard : IEntrance
    {
        public Guid Id { get; set; }
        public Guid AccessCardId { get; set; }

        public DateOnly EntranceDate { get; set; }
        public TimeOnly EntranceEntryTime { get; set; }
        public TimeOnly? EntranceExitTime { get; set; }


        // 🔹 Implementación de IEntrance
        public DateTime EntryTime
        {
            get => EntranceDate.ToDateTime(EntranceEntryTime);
            set
            {
                EntranceDate = DateOnly.FromDateTime(value);
                EntranceEntryTime = TimeOnly.FromDateTime(value);
            }
        }

        public DateTime? ExitTime
        {
            get => EntranceExitTime.HasValue
                ? EntranceDate.ToDateTime(EntranceExitTime.Value)
                : null;
            set => EntranceExitTime = value.HasValue
                ? TimeOnly.FromDateTime(value.Value)
                : null;
        }

        // Navigation
        public AccessCard AccessCard { get; set; }
    }
}
