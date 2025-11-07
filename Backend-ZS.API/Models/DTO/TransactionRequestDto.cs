using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionRequestDto
    {
        public Guid ClientId { get; set; }
        public Guid TransactionItemId { get; set; }
        public Guid? PaymentId { get; set; }
    }
}
