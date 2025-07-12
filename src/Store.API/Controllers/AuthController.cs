using Microsoft.AspNetCore.Mvc;
using Store.Application.Dtos.Auth;
using Store.Application.Interfaces;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger; 

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login request failed validation.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Attempting to log in user: {Username}", request.Username);


            var response = await _authService.LoginAsync(request.Username, request.Password);

            if(response == null)
            {
                _logger.LogWarning("Authentication failed for user: {Username}", request.Username);
                return Unauthorized(new { message = "Invaalid credentials." });
            }

            _logger.LogInformation("User {Username} logged in successfully.", request.Username);
            return Ok(response);
        }
    }


}
