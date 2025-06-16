using AutoMapper;
using BacklogClear.Communication.Requests.Users;
using BacklogClear.Communication.Responses.Users;
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Users;
using BacklogClear.Domain.Security.Crytography;
using BacklogClear.Domain.Security.Tokens;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;
using FluentValidation.Results;

namespace BacklogClear.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
    private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    
    public RegisterUserUseCase(
        IMapper mapper, 
        IPasswordEncrypter passwordEncrypter, 
        IUsersReadOnlyRepository usersReadOnlyRepository,
        IUsersWriteOnlyRepository usersWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _usersReadOnlyRepository = usersReadOnlyRepository;
        _usersWriteOnlyRepository = usersWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        
        await _usersWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();
        
        return new ResponseRegisteredUserJson()
        {
            Email = user.Email,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
    
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(request);

        var emailExist = await _usersReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure("Email", ResourceErrorMessages.USER_EMAIL_ALREADY_REGISTERED));
        }
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}