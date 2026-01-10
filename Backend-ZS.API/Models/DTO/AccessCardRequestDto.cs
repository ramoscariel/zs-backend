namespace Backend_ZS.API.Models.DTO
{
    public class AccessCardRequestDto : TransactionItemRequestDto
    {
        public string HolderName { get; set; } = "";
        public double Total { get; set; }
        public int Uses { get; set; } = 10;
    }
}
