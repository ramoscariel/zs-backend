namespace Backend_ZS.API.Models.DTO
{
    public class TransactionItemDto
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public string TransactionType { get; set; }
    }
}
