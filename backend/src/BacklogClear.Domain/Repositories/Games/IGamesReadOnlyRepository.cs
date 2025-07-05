using BacklogClear.Domain.Entities;

public interface IGamesReadOnlyRepository
{
    Task<List<Game>> GetAll(User user);
    Task<Game?> GetById(User user, long id);
    //Task<List<Game>> FilterByReleaseDate(DateOnly releaseDate);
    Task<List<Game>> FilterByStartPlayingDate(DateTime initialStartDate, DateTime finalStartDate);
}