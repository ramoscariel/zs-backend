namespace Backend_ZS.API.Models.Domain
{
    public class BarProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
    }
}
