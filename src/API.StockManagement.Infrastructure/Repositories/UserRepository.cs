using API.StockManagement.Domain.Entities;
using API.StockManagement.Infrastructure.Abstractions.Repositories;
using API.StockManagement.Infrastructure.Persistence;

namespace API.StockManagement.Infrastructure.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<User?> GetByUsername(string username)
        {
            return await GetByConditionAsync(x => x.Username.ToLower() == username.ToLower(), nameof(Role));
        }
        public async Task<bool> AnyByUsername(string username)
        {
            return await AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}
