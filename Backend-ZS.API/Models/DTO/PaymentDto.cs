using System.Text.Json.Serialization;
using Backend_ZS.API.Models.Domain;

namespace Backend_ZS.API.Models.DTO
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Total { get; set; }
        public PaymentType Type { get; set; }
        public Guid TransactionId { get; set; }

        // Nav Props - JsonIgnore to prevent circular reference when serializing Transaction with Payments
        [JsonIgnore]
        public TransactionDto? Transaction { get; set; }
    }
}
