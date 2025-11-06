using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class BarOrderDetailCreateRequestDto
    {
        [Required]
        public Guid BarProductId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Qty { get; set; }
    }
}
