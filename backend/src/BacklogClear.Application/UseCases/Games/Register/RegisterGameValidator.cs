using BacklogClear.Communication.Requests;
using FluentValidation;

namespace BacklogClear.Application.UseCases.Games.Register;

public class RegisterGameValidator : AbstractValidator<RequestRegisterGameJson>
{
    public RegisterGameValidator()
    {
        RuleFor(game => game.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(game => game.Platform).NotEmpty().WithMessage("Platform is required.");
        RuleFor(game => game.Genre).NotEmpty().WithMessage("Genre is required.");
        RuleFor(game => game.ReleaseDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Release date must be in the past.");
        RuleFor(game => game.Status).IsInEnum().WithMessage("Status is invalid.");
    }
}