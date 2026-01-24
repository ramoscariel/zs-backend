using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string? DocumentNumber { get; set; } // opcional
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public DocumentType? DocumentType { get; set; }
    }
}
