using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionRequestDto
    {
        [Required]
        public Guid ClientId { get; set; }

        // ✅ obligatorio porque el front ya lo manda
        [Required]
        public Guid CashBoxId { get; set; }
    }
}
