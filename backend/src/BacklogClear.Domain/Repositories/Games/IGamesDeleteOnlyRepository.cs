namespace BacklogClear.Domain.Repositories.Games;

public interface IGamesDeleteOnlyRepository
{
    /// <summary>
    /// Returns true if the game was deleted, false otherwise.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}