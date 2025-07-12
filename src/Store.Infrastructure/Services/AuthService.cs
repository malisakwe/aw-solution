using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Application.Dtos.Auth;
using Store.Application.Dtos.User;
using Store.Application.Interfaces;
using Store.Application.Services;
using Store.Domain.Entities.Identity;
using System.Data.Entity.Core.Metadata.Edm;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Web.Providers.Entities;

namespace Store.Infrastructure.Services
{

    public class AuthService : Application.Interfaces.IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration; 

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
            var user = await _userRepository.GetByEmailAsync(request.Username);

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

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
//            var jwtSettings = _con
            if(username != "testuser" || password != "testpassword")
            {
                // throw new AuthenticationException("Invalid credentials");
                return null; 
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"] ?? 
                throw new InvalidOperationException("JWT Secret is not configured.");
            var issuer = jwtSettings["Issuer"] ?? 
                throw new InvalidOperationException("JWT Issuer is not configured.");   
            var audience = jwtSettings["Audience"] ??  
                throw new InvalidOperationException("JWT Audience is not configured.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "userIdFromDB"),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, "user@example.com"), 
                new Claim(ClaimTypes.Role, "User") // Assuming a default role
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            return new LoginResponse 
            {
                Token = tokenString,
                Expiration = tokenDescriptor.Expires.GetValueOrDefault(),
                Username = username
            };
        }
    }

}
