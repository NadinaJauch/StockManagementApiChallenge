using API.StockManagement.Domain.Entities;

namespace API.StockManagement.Infrastructure.Abstractions.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> AddAsync(Product entity, CancellationToken cancellationToken = default);
        public Task<bool> AnyByIdAsync(int id);
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(int id);
        public Task<bool> UpdateAsync(Product entity, CancellationToken cancellationToken = default);
        public Task<bool> DeleteById(int id);
        public Task<List<IGrouping<string, Product>>> GetProductsGroupedByCategory();
    }
}
