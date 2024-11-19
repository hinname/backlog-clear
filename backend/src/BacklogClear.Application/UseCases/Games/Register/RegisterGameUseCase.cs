using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;
using FluentValidation;

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
        var validator = new RegisterGameValidator();
        var result = validator.Validate(request);
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        var errorMessage = string.Join(" ", errorMessages);
        throw new ArgumentException(errorMessage);
    }
}