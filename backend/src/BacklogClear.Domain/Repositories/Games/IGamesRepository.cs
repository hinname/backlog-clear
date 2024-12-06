using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesRepository
{
    Task Add(Game game);
    Task<List<Game>> GetAll();
    Task<Game?> GetById(long id);
}