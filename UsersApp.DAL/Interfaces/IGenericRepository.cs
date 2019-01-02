namespace UsersApp.DAL
{
    public interface IGenericRepository<TId, TEntity>
        where TId : struct
        where TEntity : class
    {
        /*Task<TEntity> GetAsync(TId id, CancellationToken ct = default(CancellationToken));

        Task<IEnumerable<TEntity>> GetListAsync();*/
    }
}
