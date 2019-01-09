using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.BLL.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(
            CancellationToken token = default(CancellationToken));

        Task<UserDto> GetUserAsync(
            GetUserDto id,
            CancellationToken token = default(CancellationToken));

        Task<UserDto> CreateUserAsync(
            CreateUserDto user,
            CancellationToken token = default(CancellationToken));

        Task UpdateUserAsync(
            GetUserDto userId,
            UpdateUserDto updUser,
            CancellationToken token = default(CancellationToken));

        Task DeleteUserAsync(
            DeleteUserDto id,
            CancellationToken token = default(CancellationToken));

        Task<bool> UserExistsAsync(
            int id,
            CancellationToken token = default(CancellationToken));
    }
}
