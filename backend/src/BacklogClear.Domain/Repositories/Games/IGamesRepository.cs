using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesRepository
{
    Task Add(Game game);
}