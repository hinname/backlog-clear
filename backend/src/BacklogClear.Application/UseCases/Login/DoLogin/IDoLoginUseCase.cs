using BacklogClear.Communication.Requests.Login;
using BacklogClear.Communication.Responses.Users;

namespace BacklogClear.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}