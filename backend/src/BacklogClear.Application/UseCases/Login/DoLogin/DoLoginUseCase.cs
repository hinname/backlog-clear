using BacklogClear.Communication.Requests.Login;
using BacklogClear.Communication.Responses.Users;
using BacklogClear.Domain.Repositories.Users;
using BacklogClear.Domain.Security.Crytography;
using BacklogClear.Domain.Security.Tokens;
using BacklogClear.Exception.ExceptionBase;

namespace BacklogClear.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase: IDoLoginUseCase
{
    private readonly IUsersReadOnlyRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUsersReadOnlyRepository repository,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordEncrypter = passwordEncrypter;
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        await Validate(request);
        
        var user = await _repository.GetUserByEmail(request.Email);
        
        if (user is null)
        {
            throw new InvalidLoginException();
        }
        
        var passwordMatch = _passwordEncrypter.Verify(request.Password, user.Password);
        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
            Email = user.Email,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestLoginJson request)
    {
        var validator = new DoLoginValidator();
        var result = await validator.ValidateAsync(request);
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}