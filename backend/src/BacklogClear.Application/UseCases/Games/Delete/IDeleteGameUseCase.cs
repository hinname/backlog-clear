namespace BacklogClear.Application.UseCases.Games.Delete;

public interface IDeleteGameUseCase
{
    Task Execute(long id);
}