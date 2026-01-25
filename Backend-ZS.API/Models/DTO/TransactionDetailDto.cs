namespace Backend_ZS.API.Models.DTO
{
    public class TransactionDetailDto
    {
        public TransactionDto Transaction { get; set; } = null!;
        public List<EntranceTransactionDto> Entrances { get; set; } = new();
        public List<ParkingDto> Parkings { get; set; } = new();
        public List<BarOrderDto> BarOrders { get; set; } = new();
        public List<AccessCardDto> AccessCards { get; set; } = new();
        public List<KeyDto> Keys { get; set; } = new();
        public decimal TotalCharges { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal PendingBalance { get; set; }
    }
}
