using Backend_ZS.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Backend_ZS.API.Models.DTO
{
    public class TransactionRequestDto
    {
        public Guid ClientId { get; set; }
    }
}
