using Backend_ZS.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_ZS.API.Models.DTO
{
    public class KeyDto
    {
        public Guid Id { get; set; }
        public string KeyCode { get; set; }
        public bool Available { get; set; }
        public string? Notes { get; set; }

        // Nav Props
        public ClientDto? LastAssignedClient { get; set; }
    }
}
