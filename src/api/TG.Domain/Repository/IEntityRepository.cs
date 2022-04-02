using TG.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TG.Domain.Repository
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        IQueryable<TEntity> GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> Expression ,bool IgnoreGlobalFilters = false);
        Task<List<TEntity>> GetAllAsync(bool IgnoreGlobalFilters = false);
        Task<List<TEntity>> GetAllAsNoTrackingAsync(bool IgnoreGlobalFilters = false);
        IQueryable<TEntity> GetAllAsQueryable(bool IgnoreGlobalFilters = false);
        IQueryable<TEntity> GetAllAsNoTrackingQueryable(bool IgnoreGlobalFilters = false);
        Task<TEntity> GetSingleAsync(string Id, bool IgnoreGlobalFilters = false);
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(string Id);
        Task BulkInsert(List<TEntity> entities);
        Task<int> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false);
        Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false);
        bool Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> Expression,
            bool IgnoreGlobalFilters = false);
    }
}
