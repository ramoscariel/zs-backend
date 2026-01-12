// Models/Domain/TransactionItem.cs
namespace Backend_ZS.API.Models.Domain
{
    public abstract class TransactionItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public double Total { get; set; }

        // ✅ FIX: siempre debe tener valor (DB no permite NULL)
        public string TransactionType { get; set; } = string.Empty;

        public Guid? TransactionId { get; set; }      // ✅ nullable
        public Transaction? Transaction { get; set; } // ✅ nullable
    }
}
