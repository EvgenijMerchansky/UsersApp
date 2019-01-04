using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserService> _log;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            ILogger<UserService> log,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _log = log;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(
            CancellationToken token = default(CancellationToken))
        {
            _log.LogInformation("Get all users at: {datetime}", DateTime.UtcNow);

            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAll(token);

            IEnumerable<UserDto> mappedUsers = users.Select(x =>
                _mapper.Map<User, UserDto>(x))
                .ToList();

            return mappedUsers;
        }

        public async Task<UserDto> GetUserAsync(
            GetUserDto getUser,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                _log.LogInformation("Get user by Id={id}", getUser.Id);

                User user = await _unitOfWork.UserRepository.GetAsync(getUser.Id, token);

                UserDto mappedUser = _mapper.Map<User, UserDto>(user);

                return mappedUser;
            }
            catch (Exception e)
            {
                _log.LogError(e, "Failed to get user by Id");
                throw;
            }
        }

        public async Task CreateUserAsync(
            CreateUserDto user,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                _log.LogInformation("Creating new user {FirstName} has started", user.FirstName);

                User mappedUser = _mapper.Map<CreateUserDto, User>(user);

                await _unitOfWork.UserRepository.CreateAsync(mappedUser, token);

                await _unitOfWork.CommitAsync(token);
            }
            catch (Exception e)
            {
                _log.LogInformation(e, "Failed to create new user");
                throw;
            }
        }

        public async Task UpdateUserAsync(
            GetUserDto userId,
            UpdateUserDto updUser,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                _log.LogInformation("Updating existing user with id={id} has started", userId.Id);

                User mappedUser = _mapper.Map<UpdateUserDto, User>(updUser);

                User exUser = await _unitOfWork.UserRepository.GetAsync(userId.Id);

                User updatedUser = _mapper.Map(mappedUser, exUser);

                updatedUser.Id = userId.Id;

                _unitOfWork.UserRepository.Update(userId.Id, updatedUser);

                await _unitOfWork.CommitAsync(token);
            }
            catch (Exception e)
            {
                _log.LogError(e, "Failed to update user");
            }
        }

        public async Task DeleteUserAsync(
            DeleteUserDto deleteUser,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                _log.LogInformation("Deleting existing user with id={id} has started", deleteUser.Id);

                User mappedUser = _mapper.Map<DeleteUserDto, User>(deleteUser);

                await _unitOfWork.UserRepository.DeleteAsync(mappedUser.Id, token);

                await _unitOfWork.CommitAsync(token);
            }
            catch (Exception e)
            {
                _log.LogError(e, "Failed to delete user");
            }
        }

        public async Task<bool> UserExistsAsync(int id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                _log.LogInformation("Check if exists user with id={id} exists", id);

                return await _unitOfWork.UserRepository.UserExistsAsync(id);
            }
            catch (Exception e)
            {
                _log.LogError(e, "Failed to get user by Id");
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
