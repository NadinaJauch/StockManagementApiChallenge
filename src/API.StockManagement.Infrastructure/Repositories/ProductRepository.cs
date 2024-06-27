using API.StockManagement.Domain.Entities;
using API.StockManagement.Infrastructure.Abstractions.Repositories;
using API.StockManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.StockManagement.Infrastructure.Repositories
{
    internal class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        protected readonly DbSet<Product> _entity = context.Set<Product>();

        public async Task<bool> AnyByIdAsync(int id)
        {
            return await AnyAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await DeleteAsync(x => x.Id == id);
        }

        public async Task<List<IGrouping<string,Product>>> GetProductsGroupedByCategory()
        {
            return await _entity.GroupBy(p => p.Category).ToListAsync();
        }
    }
}
