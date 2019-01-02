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
        /// <example>api/Users/5</example>
        /// <response code="200">Success</response>
        /// <response code="400">If user not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserDto>> Get([FromRoute]GetUserDto getUser)
        {
            UserDto user = await _userService.GetUserAsync(getUser);

            return Ok(user);
        }

        /// <summary>Create new user</summary>
        /// <type>POST</type>
        /// <example>api/Users</example>
        /// <response code="200">Success</response>
        /// <response code="404">If data in the request body is wrong</response>
        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task Post([FromBody]CreateUserDto user)
        {
            await _userService.CreateUserAsync(user);
        }

        /// <summary>Update current user's data</summary>
        /// <type>PUT</type>
        /// <example>api/Users/5</example>
        /// <response code="200">Success</response>
        /// <response code="404">If data in the request body is wrong</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        public async Task Put(int id, [FromBody]UpdateUserDto updateUser)
        {
             await _userService.UpdateUserAsync(id, updateUser);
        }

        /// <summary>Delete existing user</summary>
        /// <type>DELETE</type>
        /// <example>api/Users/5</example>
        /// <response code="200">Success</response>
        /// <response code="400">If existing user not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task Delete([FromRoute]DeleteUserDto deleteUser)
        {
            await _userService.DeleteUserAsync(deleteUser);
        }
    }
}
