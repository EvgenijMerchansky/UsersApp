using FluentValidation;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.BLL.Validation
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserDto>
    {
        public DeleteUserValidator(IUserService userService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Id)
                .MustAsync(async (id, token) =>
                await userService.UserExistsAsync(id, token));
        }
    }
}
