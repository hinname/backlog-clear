using BacklogClear.Communication.Responses.Games;

namespace BacklogClear.Application.UseCases.Games.GetAll;

public interface IGetAllGamesUseCase
{
    public Task<ResponseGamesJson> Execute();
}