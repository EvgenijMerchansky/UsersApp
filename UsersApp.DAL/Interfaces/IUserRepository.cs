using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.DAL.Models;

namespace UsersApp.DAL
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(
            CancellationToken token = default(CancellationToken));

        Task<User> GetUserAsync(
            int id,
            CancellationToken token = default(CancellationToken));

        Task CreateUserAsync(
            User user,
            CancellationToken token = default(CancellationToken));

        Task UpdateUserAsync(
            int id,
            User updatedUser,
            CancellationToken token = default(CancellationToken));

        Task DeleteUserAsync(
            User user,
            CancellationToken token = default(CancellationToken));

        Task<bool> UserExistsAsync(
            int id,
            CancellationToken ct = default(CancellationToken));
    }
}
