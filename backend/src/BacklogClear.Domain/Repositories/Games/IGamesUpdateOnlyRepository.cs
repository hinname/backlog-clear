using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesUpdateOnlyRepository
{
    Task<Game?> GetById(long id);
    void Update(Game game);
}