using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class OpenCashBoxRequestDto
    {
        [Required]
        public double OpeningBalance { get; set; }
    }
}
