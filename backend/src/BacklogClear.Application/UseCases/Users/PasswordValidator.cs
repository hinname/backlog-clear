using BacklogClear.Exception.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace BacklogClear.Application.UseCases.Users;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }
    
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        // RuleFor(user => user.Password)
        //     .NotEmpty().WithMessage(ResourceErrorMessages.USER_PASSWORD_REQUIRED)
        //     .MinimumLength(8).WithMessage(ResourceErrorMessages.USER_PASSWORD_INVALID)
        //     .Matches(@"[a-zA-Z]").WithMessage(ResourceErrorMessages.USER_PASSWORD_INVALID)
        //     .Matches(@"[10-9]").WithMessage(ResourceErrorMessages.USER_PASSWORD_INVALID)
        //     .Matches(@"\W").WithMessage(ResourceErrorMessages.USER_PASSWORD_INVALID);
        
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.USER_PASSWORD_REQUIRED);
            return false;
        }
        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.USER_PASSWORD_INVALID);
            return false;
        }
        if (!password.Any(char.IsLetter))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.USER_PASSWORD_INVALID);
            return false;
        }
        if (!password.Any(char.IsDigit))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.USER_PASSWORD_INVALID);
            return false;
        }
        // Check if the password contains at least one special character
        if (!password.Any(c => !char.IsLetterOrDigit(c)))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.USER_PASSWORD_INVALID);
            return false;
        }
        return true;
    }

    public override string Name => "PasswordValidator";
}