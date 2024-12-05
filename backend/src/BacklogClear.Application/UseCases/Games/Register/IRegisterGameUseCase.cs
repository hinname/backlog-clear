using BacklogClear.Communication.Requests.Games;
using BacklogClear.Communication.Responses.Games;

namespace BacklogClear.Application.UseCases.Games.Register;

public interface IRegisterGameUseCase
{
    public Task<ResponseRegisteredGameJson> Execute(RequestRegisterGameJson request);
}