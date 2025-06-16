using Store.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Dtos.Auth
{
    // Store.Application/DTOs/Auth/AuthResult.cs
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public AuthResult() { }

        public AuthResult(UserDto user, string token, string? refreshToken = null)
        {
            Success = true;
            Token = token;
            RefreshToken = refreshToken;
            User = user;
            Errors = Enumerable.Empty<string>();
        }

        public AuthResult(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
        }

        public static AuthResult SuccessResult(UserDto user, string token, string? refreshToken = null)
            => new AuthResult(user, token, refreshToken);

        public static AuthResult FailureResult(params string[] errors)
            => new AuthResult(errors);
    }
}
