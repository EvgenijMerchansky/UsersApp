using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.DAL.Models;

namespace UsersApp.DAL
{
    public interface IUserRepository : IGenericRepository<int, User>
    {
        Task<bool> UserExistsAsync(
            int id,
            CancellationToken ct = default(CancellationToken));
    }
}
