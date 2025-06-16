using AutoMapper;
using Store.Application.Dtos.Auth;
using Store.Application.Dtos.User;
using Store.Application.Interfaces;
using Store.Application.Services;
using Store.Domain.Entities.Identity;
using System.Security.Authentication;
using System.Security.Claims;

namespace Store.Infrastructure.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            // Validation
            if (await _userRepository.ExistsByUsername(request.Username))
                throw new AuthenticationException("Username already exists");

            if (await _userRepository.ExistsByEmail(request.Email))
                throw new AuthenticationException("Email already exists");

            // Create user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = "User" // Default role
            };

            await _userRepository.AddAsync(user);

            // Generate tokens
            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Update user with refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResult(
                _mapper.Map<UserDto>(user),
                token,
                refreshToken);
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new AuthenticationException("Invalid credentials");

            // Generate tokens
            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Update user with refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResult(
                _mapper.Map<UserDto>(user),
                token,
                refreshToken);
        }

        public async Task<AuthResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new AuthenticationException("User identifier claim not found.");
            var userId = int.Parse(userIdClaim);

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                throw new AuthenticationException("Invalid refresh token");
            }

            // Generate new tokens
            var newToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update user with new refresh token
            user.RefreshToken = newRefreshToken;
            await _userRepository.UpdateAsync(user);

            return new AuthResult(
                _mapper.Map<UserDto>(user),
                newToken,
                newRefreshToken);
        }
    }

}
