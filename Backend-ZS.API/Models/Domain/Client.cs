namespace Backend_ZS.API.Models.Domain
{
    public class Client
    {
        public Guid Id { get; set; }
        public string NationalId { get; set; } // cédula ecuatoriana
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
    }
}
