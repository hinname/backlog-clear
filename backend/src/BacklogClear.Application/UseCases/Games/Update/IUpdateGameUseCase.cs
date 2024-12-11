using BacklogClear.Communication.Requests.Games;

namespace BacklogClear.Application.UseCases.Games.Update;

public interface IUpdateGameUseCase
{
    Task Execute(long id, RequestGameJson request);
}