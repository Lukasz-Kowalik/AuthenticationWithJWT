using BackEnd.Entities;
using BackEnd.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BackEnd.Managers
{
    public class JwtManager : IJwtManager
    {
        private readonly byte[] _secret;
        private readonly TimeSpan _expireTimes;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IMongoRepository<User> _userRepository;

        public JwtManager(ITokenSettings settings, IOptionsMonitor<JwtBearerOptions> jwtOptions, IMongoRepository<User> userRepository)
        {
            _secret = Convert.FromBase64String(settings.Secret);
            _expireTimes = settings.ExpireTimeLimit;
            _tokenValidationParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
            _userRepository = userRepository;
        }

        private Token GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

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

                Expires = DateTime.UtcNow.Add(_expireTimes),

                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(_secret),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token ??= new Token();
            user.Token.CreationDate = DateTime.UtcNow;
            user.Token.ExpiryDate = DateTime.UtcNow.Add(_expireTimes);

            user.Token.JwtToken = tokenHandler.WriteToken(token);

            return user.Token;
        }

        public Token RefreshToken(string token, string refreshToken)
        {
            var principal = GetPrincipalFromToken(token);
            if (principal is null)
            {
                throw new Exception("Empty principal");
            }
            var user = _userRepository.GetById(principal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (user.Token.RefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }
            GenerateToken(user);
            GenerateRefreshToken(user);
            return user.Token;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var securityToken);
            if (IsJwtWithValidSecurityArgorithm(securityToken))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private bool IsJwtWithValidSecurityArgorithm(SecurityToken securityToken)
        {
            return (securityToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                StringComparison.InvariantCultureIgnoreCase);
        }

        public User GenerateRefreshToken(User user)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                user.Token.RefreshToken = Convert.ToBase64String(randomNumber);
                user.Token.CreationDate = DateTime.UtcNow;
                user.Token.ExpiryDate = DateTime.UtcNow.AddDays(7);
                _userRepository.Update(user);
                return user;
            }
        }

        public async Task<Token> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(x => x.Email.Equals(email)
            && x.Password.Equals(password));
            if (user is null) return null;

            user.Token = GenerateToken(user);
            _userRepository.Update(user);
            return user.Token;
        }
    }
}