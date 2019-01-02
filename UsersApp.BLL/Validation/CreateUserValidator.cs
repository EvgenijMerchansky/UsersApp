using FluentValidation;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.BLL.Validation
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(300);
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
