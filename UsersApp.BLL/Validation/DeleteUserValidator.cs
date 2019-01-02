using FluentValidation;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.BLL.Validation
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserDto>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
