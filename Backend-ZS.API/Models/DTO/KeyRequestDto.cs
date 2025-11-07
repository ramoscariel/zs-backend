namespace Backend_ZS.API.Models.DTO
{
    public class KeyRequestDto
    {
        public Guid? LastAssignedTo { get; set; }
        public bool Available { get; set; }
        public string? Notes { get; set; }
    }
}
