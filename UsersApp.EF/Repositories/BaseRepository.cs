using Microsoft.EntityFrameworkCore;
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

        public virtual async Task<TEntity> GetAsync(TId id, CancellationToken ct = default(CancellationToken))
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
