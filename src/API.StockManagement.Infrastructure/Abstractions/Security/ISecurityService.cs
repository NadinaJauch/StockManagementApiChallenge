using API.StockManagement.Domain.Common;
using API.StockManagement.Domain.Entities;

namespace API.StockManagement.Infrastructure.Abstractions.Identity
{
    public interface ISecurityService
    {
        public Task<Result<string?>> ValidateCredentials(string username, string password);
        public string HashPassword(string password);
        public string GenerateJwtToken(User user);
    }
}
