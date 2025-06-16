using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Add other non-sensitive fields as needed
        // public string DisplayName { get; set; }
        // public DateTime CreatedAt { get; set; }
    }
}
