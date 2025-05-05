using BacklogClear.Communication.Requests.Users;
using BacklogClear.Exception.Resources;
using FluentValidation;

namespace BacklogClear.Application.UseCases.Users.Register;

public class RegisterUserValidator: AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.USER_EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ResourceErrorMessages.USER_EMAIL_INVALID);
        RuleFor(user => user.Nickname).NotEmpty().WithMessage(ResourceErrorMessages.USER_NAME_REQUIRED);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}