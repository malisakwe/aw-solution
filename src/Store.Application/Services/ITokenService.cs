
using Store.Domain.Entities.Identity;
using System.Security.Claims;

namespace Store.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool ValidateToken(string token);

    }
}
