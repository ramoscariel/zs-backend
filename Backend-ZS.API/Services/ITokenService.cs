using Backend_ZS.API.Models.Domain;
using System.Security.Claims;

namespace Backend_ZS.API.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        DateTime GetExpiration();
        DateTime GetRefreshTokenExpiration();
    }
}
