using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Example.DBLayer.Repositories.Interfaces;

namespace Users.Example.DBLayer.Repositories;

public class BaseRepository<TId, TEntity, TContext>(TContext context) : IBaseRepository<TId, TEntity>
    where TEntity : class, new()
    where TContext : DbContext
    where TId : struct
{
    public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken ct) => await context.Set<TEntity>().ToListAsync(ct);

    public virtual async Task<TEntity> Get(TId id, CancellationToken ct) => await context.Set<TEntity>().FindAsync(id, ct);

    public virtual async Task Create(TEntity entity, CancellationToken ct) => await context.Set<TEntity>().AddAsync(entity, ct);

    public virtual async Task Update(TId id, TEntity entity, CancellationToken ct)
    {
        var existingEntity = await context.Set<TEntity>().FindAsync([id], ct);
        if (existingEntity is null) return;

        var existingEntry = context.Entry(existingEntity);
        var incomingEntry = context.Entry(entity);

        foreach (var property in existingEntry.Properties)
        {
            if (property.Metadata.IsKey()) continue;

            property.CurrentValue = incomingEntry.Property(property.Metadata.Name).CurrentValue;
        }
    }

    public virtual async Task Delete(TId id, CancellationToken ct)
    {
        TEntity entity = await context.FindAsync<TEntity>(id, ct);
        context.Set<TEntity>().Remove(entity);
    }

    public virtual Task CommitAsync(CancellationToken ct) => context.SaveChangesAsync(ct);

    public void Dispose() => context.Dispose();
}