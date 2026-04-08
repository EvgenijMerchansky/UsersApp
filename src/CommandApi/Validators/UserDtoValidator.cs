using FluentValidation;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.CommandApi.Site.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(model => model.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(100).WithMessage("FirstName must not exceed 100 characters.");

        RuleFor(model => model.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(100).WithMessage("LastName must not exceed 100 characters.");

        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("LastName is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(100).WithMessage("LastName must not exceed 100 characters.");

        RuleFor(command => command.Products)
            .NotNull()
            .WithMessage("Products list is required.");
    }
}
