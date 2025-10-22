namespace Backend_ZS.API.Models.DTO
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
    }
}
