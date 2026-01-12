using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_ZS.API.Models.Domain
{
    public class Key
    {
        public Guid Id { get; set; }
        public string KeyCode { get; set; } = null!;

        // FK real
        public Guid? LastAssignedTo { get; set; }

        public bool Available { get; set; }
        public string? Notes { get; set; }

        // Nav
        [ForeignKey(nameof(LastAssignedTo))]
        public Client? LastAssignedClient { get; set; }
    }
}
