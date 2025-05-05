using AutoMapper;
using BacklogClear.Communication.Requests.Users;
using BacklogClear.Communication.Responses.Users;
using BacklogClear.Exception.ExceptionBase;

namespace BacklogClear.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    
    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        return new ResponseRegisteredUserJson();
    }
    
    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}