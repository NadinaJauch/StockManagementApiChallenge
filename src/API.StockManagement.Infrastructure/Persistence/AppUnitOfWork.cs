using API.StockManagement.Infrastructure.Abstractions;

namespace API.StockManagement.Infrastructure.Persistence
{
    internal sealed class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public AppUnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }
    }
}
