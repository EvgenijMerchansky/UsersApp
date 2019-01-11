namespace UsersApp.Tests.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoFixture;
    using AutoMapper;
    using DeepEqual.Syntax;
    using FakeItEasy;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using UsersApp.BLL.Configurations;
    using UsersApp.BLL.DTOs.Users;
    using UsersApp.BLL.Services;
    using UsersApp.DAL;
    using UsersApp.DAL.EF.Context;
    using UsersApp.DAL.EF.Repositories;
    using UsersApp.DAL.Models;
    using Xunit;

    public class UserServiceTests
    {
        private readonly ILogger<UserService> _logger = A.Fake<ILogger<UserService>>();

        private readonly IMapper _mapper;

        private readonly UnitOfWork _unitOfWork;

        private readonly UsersContext _context;

        private Fixture _fixture = new Fixture();

        public UserServiceTests()
        {
            DbContextOptions<UsersContext> moqOptions =
                new DbContextOptionsBuilder<UsersContext>()
                    .UseInMemoryDatabase("inMemoryDatabase")
                    .EnableSensitiveDataLogging()
                    .Options;

            _context = new UsersContext(moqOptions);

            _unitOfWork = new UnitOfWork(
                _context,
                A.Fake<IUserRepository>());

            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<OrganizationProfile>();
            }).CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllUsers_ValidData_ReturnsSuccess()
        {
            // Arrange
            IEnumerable<User> mockedUsers = _fixture.Create<IEnumerable<User>>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            A.CallTo(() => _unitOfWork.UserRepository.GetAll(A<CancellationToken>._))
                .ReturnsLazily(() => mockedUsers);

            // Act
            IEnumerable<UserDto> results = await userService.GetAllUsersAsync();

            IEnumerable<UserDto> mappedUsers = _mapper
                .Map<IEnumerable<UserDto>>(mockedUsers);

            // Assert
            Assert.NotEmpty(results);

            mappedUsers.WithDeepEqual(results).Assert();
        }

        [Fact]
        public async Task GetAllUsers_ValidData_ReturnsEmptyList()
        {
            // Arrange
            List<User> empty = new List<User>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            A.CallTo(() => _unitOfWork.UserRepository.GetAll(A<CancellationToken>._))
                .ReturnsLazily(() => empty);

            // Act
            IEnumerable<UserDto> results = await userService.GetAllUsersAsync();

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public async Task GetUserById_ValidModel_ReturnsSuccess()
        {
            // Arrange
            User user = _fixture.Create<User>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            GetUserDto existUser = _fixture.Create<GetUserDto>();
            existUser.Id = user.Id;

            A.CallTo(() => _unitOfWork.UserRepository.GetAsync(user.Id, A<CancellationToken>._))
                .ReturnsLazily(() => user);

            // Act
            UserDto getExistUser = await userService.GetUserAsync(existUser, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, getExistUser.Id);
        }

        [Fact]
        public async Task GetUserById_NotExistUser_ReturnsNull()
        {
            // Arrange
            User user = _fixture.Create<User>();
            GetUserDto getUser = _fixture.Create<GetUserDto>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            A.CallTo(() => _unitOfWork.UserRepository.GetAsync(A<int>._, A<CancellationToken>._))
                .Returns((User)null);

            getUser.Id = user.Id;

            user = null;

            // Act
            UserDto returnedUser = await userService.GetUserAsync(getUser, CancellationToken.None);

            // Assert
            Assert.Null(returnedUser);
        }

        [Fact]
        public async Task CreateNewUser_ValidData_ReturnsSuccess()
        {
            // Arrange
            User user = _fixture.Create<User>();
            GetUserDto getUser = _fixture.Create<GetUserDto>();
            CreateUserDto userDto = _fixture.Create<CreateUserDto>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            A.CallTo(() => _unitOfWork.UserRepository.CreateAsync(A<User>._, A<CancellationToken>._));

            getUser.Id = user.Id;

            A.CallTo(() => _unitOfWork.UserRepository.GetAsync(getUser.Id, A<CancellationToken>._))
                .ReturnsLazily(() => user);

            // Act
            UserDto returnedUser = await userService.GetUserAsync(getUser, CancellationToken.None);

            // Assert
            Assert.Equal(user.Id, returnedUser.Id);
        }

        [Fact]
        public async Task UpdateUser_ValidData_ReturnsNotContained()
        {
            // Arrange
            User user = _fixture.Create<User>();
            User updUser = _fixture.Create<User>();
            GetUserDto getUser = _fixture.Create<GetUserDto>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            getUser.Id = user.Id;
            updUser.Id = user.Id;

            A.CallTo(() => _unitOfWork.UserRepository.CreateAsync(user, A<CancellationToken>._));

            A.CallTo(() => _unitOfWork.UserRepository.GetAsync(getUser.Id, A<CancellationToken>._))
                .ReturnsLazily(() => user);

            A.CallTo(() => _unitOfWork.UserRepository.Update(updUser.Id, updUser));

            A.CallTo(() => _unitOfWork.UserRepository.GetAsync(getUser.Id, A<CancellationToken>._))
                .ReturnsLazily(() => updUser);

            // Act
            UserDto returnedUser = await userService.GetUserAsync(getUser, CancellationToken.None);

            // Assert
            Assert.False(returnedUser.IsDeepEqual(user));
        }

        [Fact]
        public async Task DeleteUser_ValidData_ReturnsNull()
        {
            // Arrange
            GetUserDto getUser = _fixture.Create<GetUserDto>();
            CreateUserDto createUser = _fixture.Create<CreateUserDto>();
            DeleteUserDto deleteUser = _fixture.Create<DeleteUserDto>();

            UnitOfWork unitOfWork = new UnitOfWork(
                _context,
                new UserRepository(_context));

            UserService userService = new UserService(
                _logger,
                _mapper,
                unitOfWork);

            // Act
            UserDto createdUser = await userService.CreateUserAsync(createUser);
            deleteUser.Id = createdUser.Id;
            getUser.Id = createdUser.Id;
            await userService.DeleteUserAsync(deleteUser, CancellationToken.None);
            UserDto result = await userService.GetUserAsync(getUser, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CheckExistUser_ValidData_ReturnsTrue()
        {
            // Arrange
            User user = _fixture.Create<User>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            A.CallTo(() => _unitOfWork.UserRepository.UserExistsAsync(user.Id, A<CancellationToken>._))
                .ReturnsLazily(() => true);

            // Act
            bool isExist = await userService.UserExistsAsync(user.Id);

            // Assert
            Assert.True(isExist);
        }

        [Fact]
        public async Task CheckExistUser_ValidData_ReturnsFalse()
        {
            // Arrange
            User user = _fixture.Create<User>();

            UserService userService = new UserService(
                _logger,
                _mapper,
                _unitOfWork);

            A.CallTo(() => _unitOfWork.UserRepository.UserExistsAsync(user.Id, A<CancellationToken>._))
                .ReturnsLazily(() => false);

            // Act
            bool isExist = await userService.UserExistsAsync(user.Id);

            // Assert
            Assert.False(isExist);
        }
    }
}
