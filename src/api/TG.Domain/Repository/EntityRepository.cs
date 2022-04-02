using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TG.Core.Entity;
using TG.Domain.Context;

namespace TG.Domain.Repository
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        private readonly AppDbContext context;

        public EntityRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false)
        {
            return await context.Set<TEntity>().AnyAsync(Expression);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>();

            if (IgnoreGlobalFilters)
                return await temp.FirstOrDefaultAsync(Expression);
            else
                return await temp.Where(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active).FirstOrDefaultAsync(Expression);
        }

        public async Task BulkInsert(List<TEntity> entities)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilter = false)
        {
            return await context.Set<TEntity>().CountAsync(Expression);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            entity.RecordStatus = Core.Enums.Enums.RecordStatus.Deleted;
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(string Id)
        {
            var record = await this.GetSingleAsync(Id);
            await this.DeleteAsync(record);
        }

        public async Task<List<TEntity>> GetAllAsNoTrackingAsync(bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>();

            if (IgnoreGlobalFilters)
                return await temp.AsNoTracking().ToListAsync<TEntity>();
            else
                return await temp.AsNoTracking().Where(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active).ToListAsync<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsQueryable(bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>().AsQueryable();

            if (IgnoreGlobalFilters)
                return temp;
            else
                return temp.Where(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active);
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>().AsQueryable();

            if (IgnoreGlobalFilters)
                return temp.Where(Expression);
            else
                return temp.Where(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active).Where(Expression);
        }

        public async Task<List<TEntity>> GetAllAsync(bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>();

            if (IgnoreGlobalFilters)
                return await temp.ToListAsync<TEntity>();
            else
                return await temp.Where(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active).ToListAsync<TEntity>();
        }

        public async Task<TEntity> GetSingleAsync(string Id, bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>();

            if (IgnoreGlobalFilters)
                return await temp.FirstOrDefaultAsync<TEntity>(x => x.ID == Id);
            else
                return await temp.FirstOrDefaultAsync<TEntity>(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active && x.ID == Id);
        }

        public IQueryable<TEntity> GetAllAsNoTrackingQueryable(bool IgnoreGlobalFilters = false)
        {
            var temp = context.Set<TEntity>().AsQueryable();

            if (IgnoreGlobalFilters)
                return temp.AsNoTracking();
            else
                return temp.AsNoTracking().Where(x => x.RecordStatus == Core.Enums.Enums.RecordStatus.Active);
        }

        public bool Any(Expression<Func<TEntity, bool>> Expression, bool IgnoreGlobalFilters = false)
        {
            return context.Set<TEntity>().Any(Expression);
        }
    }
}
