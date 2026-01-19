namespace Backend_ZS.API.Models.Domain
{
    public interface ISession
    {
        DateTime OpenedAt { get; set; }
        DateTime? ClosedAt { get; set; }
    }
}
