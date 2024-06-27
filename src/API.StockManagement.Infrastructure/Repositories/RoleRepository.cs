using API.StockManagement.Domain.Entities;
using API.StockManagement.Infrastructure.Abstractions.Repositories;
using API.StockManagement.Infrastructure.Persistence;

namespace API.StockManagement.Infrastructure.Repositories
{
    internal class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
    {
        public async Task<bool> AnyById(int id)
        {
            return await AnyAsync(x => x.Id == id);
        }
    }
}
