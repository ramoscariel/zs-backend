using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class PaymentRequestDto
    {
        [Required]
        public double Total { get; set; }
    }
}