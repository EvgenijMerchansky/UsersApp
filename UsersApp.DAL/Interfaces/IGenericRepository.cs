using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UsersApp.DAL
{
    public interface IGenericRepository<TId, TEntity>
        where TId : struct
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(
            CancellationToken token = default(CancellationToken));

        Task<TEntity> GetAsync(
            TId id,
            CancellationToken token = default(CancellationToken));

        Task CreateAsync(
            TEntity entity,
            CancellationToken token = default(CancellationToken));

        void Update(TId id, TEntity entity);

        Task DeleteAsync(
            TId id,
            CancellationToken token = default(CancellationToken));
    }
}
