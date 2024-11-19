using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;

namespace BacklogClear.Application.UseCases.Games.Register;

public class RegisterGameUseCase
{
    public ResponseRegisteredGameJson Execute(RequestRegisterGameJson request)
    {
        Validate(request);
        return new ResponseRegisteredGameJson();
    }
    
    private void Validate(RequestRegisterGameJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Title is required");
        }
        if (string.IsNullOrWhiteSpace(request.Platform))
        {
            throw new ArgumentException("Platform is required");
        }
        if (string.IsNullOrWhiteSpace(request.Genre))
        {
            throw new ArgumentException("Genre is required");
        }
        if (DateTime.Compare(request.ReleaseDate, DateTime.UtcNow) > 0)
        {
            throw new ArgumentException("Release date must be in the past");
        }
        if (!Enum.IsDefined(typeof(Status), request.Status))
        {
            throw new ArgumentException("Status is invalid");
        }
    }
}