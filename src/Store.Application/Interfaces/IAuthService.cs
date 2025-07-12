using Store.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string username, string password);
    }
}
