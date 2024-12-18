using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests.Games;
using BacklogClear.Exception.Resources;
using FluentValidation;

namespace BacklogClear.Application.UseCases.Games.Register;

public class GameValidator : AbstractValidator<RequestGameJson>
{
    public GameValidator()
    {
        RuleFor(game => game.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(game => game.Platform).NotEmpty().WithMessage(ResourceErrorMessages.PLATFORM_REQUIRED);
        RuleFor(game => game.Genre).NotEmpty().WithMessage(ResourceErrorMessages.GENRE_REQUIRED);
        RuleFor(game => game.ReleaseDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.RELEASE_DATE_MUST_BE_IN_PAST);
        RuleFor(game => game.Status).IsInEnum().WithMessage(ResourceErrorMessages.STATUS_INVALID);
        RuleFor(game => game.StartPlayingDate).Empty().When(game => game.Status == Status.Backlog).WithMessage(ResourceErrorMessages.START_DATE_NOT_ALLOWED);
        RuleFor(game => game.StartPlayingDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.START_DATE_MUST_BE_IN_PAST);
        RuleFor(game => game.EndPlayingDate).Empty().When(game => game.Status is Status.Backlog or Status.Playing).WithMessage(ResourceErrorMessages.END_DATE_NOT_ALLOWED);
        RuleFor(game => game.EndPlayingDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.END_DATE_MUST_BE_IN_PAST);
        
    }
}