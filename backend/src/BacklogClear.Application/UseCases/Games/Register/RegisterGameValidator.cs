using BacklogClear.Communication.Requests.Games;
using BacklogClear.Exception.Resources;
using FluentValidation;

namespace BacklogClear.Application.UseCases.Games.Register;

public class RegisterGameValidator : AbstractValidator<RequestRegisterGameJson>
{
    public RegisterGameValidator()
    {
        RuleFor(game => game.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(game => game.Platform).NotEmpty().WithMessage(ResourceErrorMessages.PLATFORM_REQUIRED);
        RuleFor(game => game.Genre).NotEmpty().WithMessage(ResourceErrorMessages.GENRE_REQUIRED);
        RuleFor(game => game.ReleaseDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.RELEASE_DATE_MUST_BE_IN_PAST);
        RuleFor(game => game.Status).IsInEnum().WithMessage(ResourceErrorMessages.STATUS_INVALID);
    }
}