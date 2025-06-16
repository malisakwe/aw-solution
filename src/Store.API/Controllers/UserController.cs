using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Dtos.Auth;
using Store.Application.Services;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResult>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register")]
public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterRequest request)
{
    var result = await _authService.RegisterAsync(request);
    
    if (!result.Success)
        return BadRequest(result);

    return CreatedAtAction(nameof(Login), result);
}


    }
}
