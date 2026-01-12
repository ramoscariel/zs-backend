using Backend_ZS.API.Models.DTO;

namespace Backend_ZS.API.Models.DTO
{
    public class KeyDto
    {
        public Guid Id { get; set; }
        public string KeyCode { get; set; } = string.Empty;
        public bool Available { get; set; }
        public string? Notes { get; set; }

        // útil para debug/consumo frontend si quieres
        public Guid? LastAssignedTo { get; set; }

        public ClientDto? LastAssignedClient { get; set; }
    }
}
