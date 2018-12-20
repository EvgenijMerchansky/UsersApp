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
        [Route("list")]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        // GET: api/Users/?id=5
        [HttpGet()]
        public async Task<ActionResult<UserDto>> Get([FromQuery]GetUserDto getUser)
        {
            UserDto user = await _userService.GetUserAsync(getUser);

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task Post([FromBody]CreateUserDto user)
        {
            await _userService.CreateUserAsync(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]UpdateUserDto updateUser)
        {
             await _userService.UpdateUserAsync(id, updateUser);
        }

        // DELETE: api/Users/?id=5
        [HttpDelete]
        public async Task Delete([FromQuery]DeleteUserDto deleteUser)
        {
            await _userService.DeleteUserAsync(deleteUser);
        }
    }
}
