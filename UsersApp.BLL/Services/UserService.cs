using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.DTOs.Users;
using UsersApp.DAL;
using UsersApp.DAL.Models;

namespace UsersApp.BLL.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(
            CancellationToken token = default(CancellationToken))
        {
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllUsersAsync();

            IEnumerable<UserDto> mappedUsers = users.Select(x =>
            _mapper.Map<User, UserDto>(x))
            .ToList();

            return mappedUsers;
        }

        public async Task<UserDto> GetUserAsync(
            GetUserDto getUser,
            CancellationToken token = default(CancellationToken))
        {
            User user = await _unitOfWork.UserRepository.GetUserAsync(getUser.Id);

            UserDto mappedUser = _mapper.Map<User, UserDto>(user);

            return mappedUser;
        }

        public async Task CreateUserAsync(
            CreateUserDto user,
            CancellationToken token = default(CancellationToken))
        {
            User mappedUser = _mapper.Map<CreateUserDto, User>(user);

            await _unitOfWork.UserRepository.CreateUserAsync(mappedUser, token);
        }

        public async Task UpdateUserAsync(
            int id,
            UpdateUserDto updUser,
            CancellationToken token = default(CancellationToken))
        {
            User mappedUser = _mapper.Map<UpdateUserDto, User>(updUser);

            await _unitOfWork.UserRepository.UpdateUserAsync(id, mappedUser);
        }

        public async Task DeleteUserAsync(
            DeleteUserDto deleteUser,
            CancellationToken token = default(CancellationToken))
        {
            User mappedUser = _mapper.Map<DeleteUserDto, User>(deleteUser);

            await _unitOfWork.UserRepository.DeleteUserAsync(mappedUser);
        }

        public async Task<bool> UserExistsAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return await _unitOfWork.UserRepository.UserExistsAsync(id);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        
    }
}
