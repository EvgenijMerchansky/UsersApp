using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>Get existing users list</summary>
        /// <type>GET</type>
        /// <example>api/Users</example>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IEnumerable<UserDto>> Get()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();

            return users;
        }

        /// <summary>Get existing user</summary>
        /// <type>GET</type>
        /// <param name="getUser">
        /// <see cref="GetUserDto"/>
        /// </param>
        /// <example>api/Users/5</example>
        /// <response code="200">Success</response>
        /// <response code="400">If user not found</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> Get([FromRoute]GetUserDto getUser)
        {
            UserDto user = await _userService.GetUserAsync(getUser);

            return Ok(user);
        }

        /// <summary>Create new user</summary>
        /// <type>POST</type>
        /// <param name="createUser">
        /// <see cref="CreateUserDto"/>
        /// </param>
        /// <example>api/Users</example>
        /// <response code="200">Success</response>
        /// <response code="400">If data in the request body is wrong</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task Post([FromBody]CreateUserDto createUser)
        {
            await _userService.CreateUserAsync(createUser);
        }

        /// <summary>Update current user's data</summary>
        /// <type>PUT</type>
        /// <param name="userId">
        /// <see cref="GetUserDto"/>
        /// </param>
        /// <param name="updateUser">
        /// <see cref="UpdateUserDto"/>
        /// </param>
        /// <example>api/Users/5</example>
        /// <response code="200">Success</response>
        /// <response code="400">If data in the request body is wrong</response>
        [HttpPut("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task Put([FromRoute]GetUserDto userId, [FromBody]UpdateUserDto updateUser)
        {
             await _userService.UpdateUserAsync(userId, updateUser);
        }

        /// <summary>Delete existing user</summary>
        /// <type>DELETE</type>
        /// <param name="deleteUser">
        /// <see cref="DeleteUserDto"/>
        /// </param>
        /// <example>api/Users/5</example>
        /// <response code="200">Success</response>
        /// <response code="400">If existing user not found</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task Delete([FromRoute]DeleteUserDto deleteUser)
        {
            await _userService.DeleteUserAsync(deleteUser);
        }
    }
}
