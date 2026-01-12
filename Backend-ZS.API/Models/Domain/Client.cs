namespace Backend_ZS.API.Models.Domain
{
    public class Client
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? NationalId { get; set; } // opcional
        public string? Email { get; set; }      // opcional
        public string? Address { get; set; }    // opcional
        public string? Number { get; set; }     // opcional

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
