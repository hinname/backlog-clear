using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;

namespace BacklogClear.Application.UseCases.Games.Delete;

public class DeleteGameUseCase : IDeleteGameUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGamesDeleteOnlyRepository _repository;
    
    public DeleteGameUseCase(IUnitOfWork unitOfWork, 
        IGamesDeleteOnlyRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    
    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
        {
            throw new NotFoundException(ResourceErrorMessages.GAME_NOT_FOUND);
        }
        
        await _unitOfWork.Commit();
    }
}