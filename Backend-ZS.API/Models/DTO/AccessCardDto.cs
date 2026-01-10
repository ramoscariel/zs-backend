namespace Backend_ZS.API.Models.DTO
{
    public class AccessCardDto : TransactionItemDto
    {
        public string HolderName { get; set; } = "";
        public int Uses { get; set; } = 10;
    }
}
