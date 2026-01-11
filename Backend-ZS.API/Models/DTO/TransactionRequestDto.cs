using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionRequestDto
    {
        [Required]
        public Guid ClientId { get; set; }
    }
}
