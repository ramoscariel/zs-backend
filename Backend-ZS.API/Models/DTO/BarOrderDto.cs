using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class BarOrderDto
    {
        public Guid Id { get; set; }
        public double Total { get; set; }

        // Navigation Property
        public ICollection<BarOrderDetail> Details { get; set; }
    }
}
