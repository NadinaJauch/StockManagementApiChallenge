using API.StockManagement.Domain.Entities;

namespace API.StockManagement.Infrastructure.Abstractions.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetByUsername(string username);
        public Task<bool> AnyByUsername(string username);
        public Task<bool> AddAsync(User entity, CancellationToken cancellationToken = default);
    }
}
