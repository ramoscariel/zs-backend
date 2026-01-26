using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_ZS.API.Models.Domain
{
    public class EntranceAccessCard : IEntrance
    {
        public Guid Id { get; set; }
        public Guid AccessCardId { get; set; }

        [NotMapped]
        public DateOnly EntranceDate { get; set; }

        [NotMapped]
        public TimeOnly EntranceEntryTime { get; set; }

        [NotMapped]
        public TimeOnly? EntranceExitTime { get; set; }

        [Column("EntryTime", TypeName = "datetime2")]
        public DateTime EntryTime
        {
            get => EntranceDate.ToDateTime(EntranceEntryTime);
            set
            {
                EntranceDate = DateOnly.FromDateTime(value);
                EntranceEntryTime = TimeOnly.FromDateTime(value);
            }
        }

        [Column("ExitTime", TypeName = "datetime2")]
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
