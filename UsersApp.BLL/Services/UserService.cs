using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.DTOs.Users;
using UsersApp.EF.Models;

namespace UsersApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(
            CreateUserDto user, 
            CancellationToken token = default(CancellationToken))
        {
            // some info logger

            User mappedUser = _mapper.Map<CreateUserDto, User>(user);

            await _userRepository.CreateUserAsync(mappedUser, token);
        }

        public Task DeleteUserAsync(
            GetUserDto id, 
            CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserAsync(
            GetUserDto id, 
            CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateUserAsync(
            UserDto updUser, 
            CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
