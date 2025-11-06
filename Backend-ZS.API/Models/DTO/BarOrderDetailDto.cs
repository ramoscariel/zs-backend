using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class BarOrderDetailDto
    {
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }

        // Navigation Properties
        public BarOrder BarOrder { get; set; }
        public BarProduct BarProduct { get; set; }
    }
}
