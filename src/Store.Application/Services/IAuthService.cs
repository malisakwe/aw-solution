using Store.Application.Dtos.Auth;

namespace Store.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterRequest request);
        Task<AuthResult> LoginAsync(LoginRequest request);
        Task<AuthResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
