namespace Backend_ZS.API.Models.DTO
{
    public class KeyDto
    {
        public Guid Id { get; set; }
        public string KeyCode { get; set; } = string.Empty;
        public bool Available { get; set; }
        public string? Notes { get; set; }

        public Guid? TransactionId { get; set; }
        public DateTime? LastAssignedAt { get; set; }

        public TransactionDto? Transaction { get; set; }
    }
}
