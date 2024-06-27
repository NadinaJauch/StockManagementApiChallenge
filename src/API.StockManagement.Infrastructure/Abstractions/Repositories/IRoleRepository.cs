namespace API.StockManagement.Infrastructure.Abstractions.Repositories
{
    public interface IRoleRepository
    {
        public Task<bool> AnyById(int id);
    }
}
