using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class CloseCashBoxRequestDto
    {
        // opcional: si no mandas, se calcula desde pagos
        public double? ClosingBalance { get; set; }

        // si quieres: cuadre manual (comentario)
        public string? Notes { get; set; }
    }
}
