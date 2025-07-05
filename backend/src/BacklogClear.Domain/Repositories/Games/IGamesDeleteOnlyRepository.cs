using BacklogClear.Domain.Entities;

namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesDeleteOnlyRepository
{
    Task Delete(long id);
}