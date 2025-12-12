using Backend_ZS.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class PaymentRequestDto
    {
        [Required]
        public double Total { get; set; }
        [Required]
        public PaymentType Type { get; set; }
        [Required]
        public Guid TransactionId { get; set; }
    }
}