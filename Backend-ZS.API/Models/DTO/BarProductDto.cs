namespace Backend_ZS.API.Models.DTO
{
    public class BarProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
    }
}
