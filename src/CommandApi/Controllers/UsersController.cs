using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Example.Models.Dtos.Users;
using Users.Example.Services.Services;

namespace Users.Example.CommandApi.Site.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, IServiceProvider serviceProvider, IMockUserService userService) : BaseController(logger, serviceProvider) // IMediator mediator, IUserService userService
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await userService.Get(id);
        if (user is not null) return Ok(user);
        
        logger.LogInformation("Method {caller}; user with id {id} - was not found.", nameof(Get), id);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        var errors = ValidateSingle(userDto);
        if (errors.Count > 0)
        {
            logger.LogInformation("Method {caller}; validation errors: {errors}.", nameof(Create), string.Join(",", errors));
            return BadRequest(errors);
        }

        await userService.Create(userDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
    {
        var errors = ValidateSingle(userDto);
        if (errors.Count > 0)
        {
            logger.LogInformation("Method {caller}; validation errors: {errors}.", nameof(Put), string.Join(",", errors));
            return BadRequest(errors);
        }

        await userService.Update(id, userDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await userService.Delete(id);
        return Ok();
    }
}