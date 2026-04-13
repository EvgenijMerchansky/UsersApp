using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Example.DBLayer.Repositories.Interfaces;

public interface IBaseRepository<TId, TEntity> where TId : struct where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll(CancellationToken ct = default);
    Task<TEntity> Get(TId id, CancellationToken ct = default);
    Task Create(TEntity entity, CancellationToken ct = default);
    void Update(TId id, TEntity entity);
    Task Delete(TId id, CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
}