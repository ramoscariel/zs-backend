using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionRequestDto
    {
        [Required]
        public Guid TransactionItemId { get; set; }
        [Required]
        public Guid PaymentId { get; set; }
    }
}
