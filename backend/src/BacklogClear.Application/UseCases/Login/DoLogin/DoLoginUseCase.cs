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
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUsersReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);
        
        if (user is null)
        {
            throw new InvalidLoginException();
        }
        
        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);
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
}