using BackEnd.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackEnd.Managers
{
    public class JwtManager : IJwtManager
    {
        private readonly byte[] _secret;
        private readonly int _expireTimes;

        public JwtManager(ITokenSettings settings)
        {
            _secret = Convert.FromBase64String(settings.Secret);
            _expireTimes = settings.ExpireTimeInMinutes;
        }

        public string GenerateTocken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                   {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname,user.Surname),
                        new Claim(ClaimTypes.Email,user.Email)
                   }
                ),

                Expires = now.AddMinutes(Convert.ToInt32(_expireTimes)),

                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(_secret),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}