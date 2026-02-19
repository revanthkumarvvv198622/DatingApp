using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions 
{
    public static UserDto ToUserDTO(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user),
            Email = user.Email
        };
    }
}
