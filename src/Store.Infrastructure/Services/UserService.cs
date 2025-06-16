using Store.Application.Interfaces;
using Store.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace Store.Infrastructure.Services
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        public User Authenticate(string username, string password)
        {
            // Implement authentication logic here
            throw new NotImplementedException();
        }

        User IUserService.Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
