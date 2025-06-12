using BacklogClear.Application.UseCases.Users;
using BacklogClear.Communication.Requests.Login;
using BacklogClear.Exception.Resources;
using FluentValidation;

namespace BacklogClear.Application.UseCases.Login.DoLogin;

public class DoLoginValidator : AbstractValidator<RequestLoginJson>
{
    public DoLoginValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.USER_EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ResourceErrorMessages.USER_EMAIL_INVALID);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestLoginJson>());
    }
}