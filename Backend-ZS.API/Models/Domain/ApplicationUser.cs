using Microsoft.AspNetCore.Identity;

namespace Backend_ZS.API.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
