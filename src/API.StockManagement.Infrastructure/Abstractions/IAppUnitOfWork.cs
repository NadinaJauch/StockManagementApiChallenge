namespace API.StockManagement.Infrastructure.Abstractions
{
    public interface IAppUnitOfWork
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
