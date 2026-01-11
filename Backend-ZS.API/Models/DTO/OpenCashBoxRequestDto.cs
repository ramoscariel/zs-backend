using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class OpenCashBoxRequestDto
    {
        // Si no mandas fecha, se usa "hoy"
        public DateOnly? BusinessDate { get; set; }

        [Required]
        public double OpeningBalance { get; set; }
    }
}
