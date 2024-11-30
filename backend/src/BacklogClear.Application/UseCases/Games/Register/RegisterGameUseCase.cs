using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Exception.ExceptionBase;

namespace BacklogClear.Application.UseCases.Games.Register;

public class RegisterGameUseCase : IRegisterGameUseCase
{
    private readonly IGamesRepository _repository;
    public RegisterGameUseCase(IGamesRepository repository)
    {
        _repository = repository;
    }
    public ResponseRegisteredGameJson Execute(RequestRegisterGameJson request)
    {
        Validate(request);
        var entity = new Game()
        {
            Title = request.Title,
            Platform = request.Platform,
            Genre = request.Genre,
            ReleaseDate = request.ReleaseDate,
            Status = (Domain.Enums.Status)request.Status
        };
        _repository.Add(entity);
        return new ResponseRegisteredGameJson();
    }
    
    private void Validate(RequestRegisterGameJson request)
    {
        var validator = new RegisterGameValidator();
        var result = validator.Validate(request);
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}