using Backend_ZS.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class ClientRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? DocumentNumber { get; set; } // opcional

        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }

        [StringLength(10)]
        public string? Number { get; set; }

        public DocumentType? DocumentType { get; set; }
    }
}
