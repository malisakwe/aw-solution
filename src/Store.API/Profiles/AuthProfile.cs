using AutoMapper;
using Store.Application.Dtos.User;
using Store.Domain.Entities.Identity;

namespace Store.API.Profiles
{
    public class AuthProfile: Profile
    {
        public AuthProfile()
        {
            CreateMap<User, UserDto>();
            // Add other mappings if needed
        }
    }
}
