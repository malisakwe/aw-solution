using System.ComponentModel.DataAnnotations;

namespace Store.Application.Dtos.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
