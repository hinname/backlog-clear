using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesWriteOnlyRepository
{
    Task Add(Game game);
}