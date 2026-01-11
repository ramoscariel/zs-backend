using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class CashBoxDto
    {
        public Guid Id { get; set; }
        public DateOnly BusinessDate { get; set; }
        public CashBoxStatus Status { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public double OpeningBalance { get; set; }
        public double? ClosingBalance { get; set; }
    }
}
