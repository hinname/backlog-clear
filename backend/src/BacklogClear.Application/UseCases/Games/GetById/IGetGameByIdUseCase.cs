using BacklogClear.Communication.Responses.Games;

namespace BacklogClear.Application.UseCases.Games.GetById;

public interface IGetGameByIdUseCase
{
    Task<ResponseGameJson?> Execute(long id);
}