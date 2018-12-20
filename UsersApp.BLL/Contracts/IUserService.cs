using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.BLL.Contracts
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(
            GetUserDto id, 
            CancellationToken token = default(CancellationToken));

        Task CreateUserAsync(
            CreateUserDto user, 
            CancellationToken token = default(CancellationToken));

        Task<UserDto> UpdateUserAsync(
            UserDto updUser, 
            CancellationToken token = default(CancellationToken));

        Task DeleteUserAsync(
            GetUserDto id, 
            CancellationToken token = default(CancellationToken));
    }
}
