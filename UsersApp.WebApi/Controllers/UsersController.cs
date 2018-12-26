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

        //GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get([FromRoute]GetUserDto getUser)
        {
            UserDto user = await _userService.GetUserAsync(getUser);

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUserDto user)
        {
            bool result = await _userService.CreateUserAsync(user);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]UpdateUserDto updateUser)
        {
             await _userService.UpdateUserAsync(id, updateUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]DeleteUserDto deleteUser)
        {
            await _userService.DeleteUserAsync(deleteUser);
        }
    }
}
