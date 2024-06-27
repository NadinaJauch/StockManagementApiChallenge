using API.StockManagement.Domain.Common;
using API.StockManagement.Domain.Entities;
using API.StockManagement.Domain.Errors;
using API.StockManagement.Infrastructure.Abstractions.Identity;
using API.StockManagement.Infrastructure.Abstractions.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.StockManagement.Infrastructure.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _userRepository;
        private readonly Token _token;
        public SecurityService(IUserRepository userRepository,
                               IOptions<Token> tokenOptions)
        {
            _userRepository = userRepository;
            _token = tokenOptions.Value;
        }

        public async Task<Result<string?>> ValidateCredentials(string username, string password)
        {
            User? user = await _userRepository.GetByUsername(username);

            if (user != null && HashPassword(password) == user.Password)
                return GenerateJwtToken(user);
            
            return Result.Failure<string?>(AuthenticationErrors.InvalidCredentials);
        }
        public string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
        public string GenerateJwtToken(User user)
        {
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
            JwtSecurityTokenHandler handler = new();
            SecurityTokenDescriptor descriptor = new()
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new("sserId", $"{user.Id}"),
                    new("username", $"{user.Username}"),
                    new("role", $"{user.Role.RoleDescription}"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
