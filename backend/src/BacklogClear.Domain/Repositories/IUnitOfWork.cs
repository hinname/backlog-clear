namespace BacklogClear.Domain.Repositories;

public interface IUnitOfWork
{
    void Commit();
}