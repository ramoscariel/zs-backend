namespace Backend_ZS.API.Models.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public double Total { get; set; }
    }
}
