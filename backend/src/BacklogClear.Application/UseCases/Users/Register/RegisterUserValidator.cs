using BacklogClear.Communication.Requests;
using FluentValidation;

namespace BacklogClear.Application.UseCases.User.Register;

public class RegisterUserValidator: AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(user => user.Nickname).NotEmpty().WithMessage("Nickname is required");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
    }
}