using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_ZS.API.Models.Domain
{
    public class Key
    {
        public Guid Id { get; set; }
        public string KeyCode { get; set; } = null!;

        // Link to transaction for POS tracking
        public Guid? TransactionId { get; set; }

        [ForeignKey(nameof(TransactionId))]
        public Transaction? Transaction { get; set; }

        public DateTime? LastAssignedAt { get; set; }

        public bool Available { get; set; }
        public string? Notes { get; set; }
    }
}
