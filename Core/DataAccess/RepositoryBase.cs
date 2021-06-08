using Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private readonly TContext context;
        public RepositoryBase(TContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await this.context.Set<TEntity>().FindAsync(id);
        }
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ?
                     this.context.Set<TEntity>().AsNoTracking() :
                     this.context.Set<TEntity>().Where(expression).AsNoTracking();
        }
        public void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            this.context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
        }
    }
}