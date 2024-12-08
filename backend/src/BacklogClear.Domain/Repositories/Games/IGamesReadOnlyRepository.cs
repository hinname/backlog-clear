using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesReadOnlyRepository
{
    Task<List<Game>> GetAll();
    Task<Game?> GetById(long id);
}