using AutoMapper;
using BacklogClear.Communication.Requests.Users;
using BacklogClear.Communication.Responses.Users;
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories.Users;
using BacklogClear.Domain.Security.Crytography;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;
using FluentValidation.Results;

namespace BacklogClear.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    
    public RegisterUserUseCase(
        IMapper mapper, 
        IPasswordEncripter passwordEncripter, 
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        return new ResponseRegisteredUserJson();
    }
    
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure("Email", ResourceErrorMessages.USER_EMAIL_ALREADY_REGISTERED));
        }
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}