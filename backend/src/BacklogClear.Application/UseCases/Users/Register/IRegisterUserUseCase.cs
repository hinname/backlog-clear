using BacklogClear.Communication.Requests.Users;
using BacklogClear.Communication.Responses.Users;

namespace BacklogClear.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}