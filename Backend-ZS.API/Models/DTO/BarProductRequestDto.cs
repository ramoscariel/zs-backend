using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class BarProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public double UnitPrice { get; set; }
    }
}
