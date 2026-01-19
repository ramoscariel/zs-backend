namespace Backend_ZS.API.Models.DTO
{
    public class TransactionItemDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Total { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public Guid? TransactionId { get; set; } // BUG #2 fix: Match domain model's nullable type
    }
}
