using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class BarOrderDto : TransactionItemDto
    {
        // Navigation Property
        public ICollection<BarOrderDetailDto> Details { get; set; }
    }
}
