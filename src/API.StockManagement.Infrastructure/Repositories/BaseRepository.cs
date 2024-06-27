using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.StockManagement.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entity;
        protected BaseRepository(DbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            T? entity = await _entity.FirstOrDefaultAsync(predicate, cancellationToken);
            if (entity == null)
                return false;
            _entity.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _entity.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.AnyAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }
        public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }
        public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate,
                                                  params string[] includeProperties)
        {
            IQueryable<T> query = _entity;

            if (includeProperties.Length > 0)
                query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));

            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetManyByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entity.Where(predicate);
            return await query.ToListAsync();
        }
        public async Task<TField> GetProjectionByConditionAsync<TField>(Expression<Func<T, bool>> predicate,
                                                                        Expression<Func<T, TField>> fieldSelector)
        {
            return await _entity.Where(predicate).Select(fieldSelector).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TField>> GetProjectionByConditionManyAsync<TField>(Expression<Func<T, bool>> predicate,
                                                                                         Expression<Func<T, TField>> fieldSelector)
        {
            return await _entity.Where(predicate).Select(fieldSelector).ToListAsync();
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }
        public void Update(T entity)
        {
            _entity.Update(entity);
        }
    }
}
