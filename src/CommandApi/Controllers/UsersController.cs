using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Example.CommandService.Commands;
using Users.Example.Models.Dtos.Users;
using Users.Example.QueryService.Queries;

namespace Users.Example.CommandApi.Site.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, IServiceProvider serviceProvider, IMediator mediator) : BaseController(logger, serviceProvider)
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await mediator.Send(new GetUserQuery(id)); 
        if (user is not null) return Ok(user);
        
        logger.LogInformation("Method {caller}; user with id {id} - was not found.", nameof(Get), id);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        var errors = ValidateSingle(createUserDto);
        if (errors.Count > 0)
        {
            logger.LogInformation("Method {caller}; validation errors: {errors}.", nameof(Create), string.Join(",", errors));
            return BadRequest(errors);
        }

        await mediator.Send(new CreateUserCommand(createUserDto));
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

        await mediator.Send(new UpdateUserCommand(id, userDto));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteUserCommand(id));
        return Ok();
    }
}