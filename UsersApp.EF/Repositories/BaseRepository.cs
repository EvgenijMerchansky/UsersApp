using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.DAL;

namespace UsersApp.EF.Repositories
{
    public class BaseRepository<TId, TEntity, TContext> : IGenericRepository<TId, TEntity>
        where TEntity : class, new()
        where TContext : DbContext
        where TId : struct
    {
        protected TContext DbContext { get; }

        public BaseRepository(TContext context)
        {
            DbContext = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(
            CancellationToken token = default(CancellationToken))
        {
            return await DbContext.Set<TEntity>()
                .ToListAsync(token);
        }

        public virtual async Task<TEntity> GetAsync(
            TId id,
            CancellationToken token = default(CancellationToken))
        {
            return await DbContext.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task CreateAsync(
            TEntity entity,
            CancellationToken token = default(CancellationToken))
        {
            await DbContext.Set<TEntity>()
                .AddAsync(entity, token);
        }

        public virtual async Task Update(TId id, TEntity entity)
        {
            DbContext.Set<TEntity>()
                .Update(entity);
        }

        public virtual async Task DeleteAsync(
            TId id, 
            CancellationToken token = default(CancellationToken))
        {
            TEntity entity = await DbContext.FindAsync<TEntity>(id);

            DbContext.Set<TEntity>()
                .Remove(entity);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
