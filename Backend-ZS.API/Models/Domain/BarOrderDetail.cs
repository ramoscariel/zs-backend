using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_ZS.API.Models.Domain
{
    public class BarOrderDetail
    {
        public Guid BarOrderId { get; set; }
        public Guid BarProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Qty { get; set; }

        // Navigation Properties
        public BarOrder BarOrder { get; set; }
        public BarProduct BarProduct { get; set; }
    }
}
