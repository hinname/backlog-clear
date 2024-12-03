namespace BacklogClear.Domain.Repositories;

public interface IUnitOfWork
{ 
    Task Commit();
}