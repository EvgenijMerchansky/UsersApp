using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsersApp.EF.Interfaces
{
    interface IGenericRepository<TId, TEntity> 
        where TId : struct 
        where TEntity : class
    {
        /*Task<TEntity> GetAsync(TId id, CancellationToken ct = default(CancellationToken));

        Task<IEnumerable<TEntity>> GetListAsync();*/
    }
}
