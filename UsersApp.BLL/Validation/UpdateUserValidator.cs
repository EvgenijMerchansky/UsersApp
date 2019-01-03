using FluentValidation;
using UsersApp.BLL.Contracts;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.BLL.Validation
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator(IUserService userService)
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(300);
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(300);

        }
    }
}
