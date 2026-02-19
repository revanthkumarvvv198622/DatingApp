using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration configuration) :ITokenService
{
   public string CreateToken(AppUser user)
   {
         var tokenKey = configuration["TokenKey"] ?? throw new ArgumentNullException("TokenKey is missing in configuration");
         if(tokenKey.Length < 64)
         {
            throw new ArgumentException("Token key must be at least 64 characters long");
         }
         
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

         var claims = new List<Claim>
         {
           new(ClaimTypes.Email, user.Email),
           new(ClaimTypes.NameIdentifier, user.Id.ToString())  
         };

         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

         var tokenDescriptor = new SecurityTokenDescriptor
         {
             Subject = new ClaimsIdentity(claims),
             SigningCredentials = creds,
             Expires = DateTime.UtcNow.AddDays(7)
         };

         var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
   }

}
