namespace Backend_ZS.API.Models.DTO
{
    public class AccessCardRequestDto : TransactionItemRequestDto
    {
        public double Total { get; set; }
        public int Uses { get; set; } = 10;
    }
}
