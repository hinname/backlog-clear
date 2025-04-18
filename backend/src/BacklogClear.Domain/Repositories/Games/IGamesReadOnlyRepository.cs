using BacklogClear.Domain.Entities;

public interface IGamesReadOnlyRepository
{
    Task<List<Game>> GetAll();
    Task<Game?> GetById(long id);
    //Task<List<Game>> FilterByReleaseDate(DateOnly releaseDate);
    Task<List<Game>> FilterByStartPlayingDate(DateTime initialStartDate, DateTime finalStartDate);
}