using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {

        protected readonly TContext Context;
        public IQueryable<TEntity> Query() => Context.Set<TEntity>();

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> queryable = Query();
            queryable = filter == null ? queryable : queryable.Where(filter);
            return await queryable.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> queryable = Query();
            return await queryable.SingleOrDefaultAsync(filter);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}
