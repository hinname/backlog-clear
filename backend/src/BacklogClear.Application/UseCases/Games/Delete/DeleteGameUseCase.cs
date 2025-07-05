using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Domain.Services.LoggedUser;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;

namespace BacklogClear.Application.UseCases.Games.Delete;

public class DeleteGameUseCase : IDeleteGameUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGamesDeleteOnlyRepository _deleteRepository;
    private readonly IGamesReadOnlyRepository _readRepository;
    private readonly ILoggedUser _loggedUser;
    
    public DeleteGameUseCase(IUnitOfWork unitOfWork, 
        IGamesDeleteOnlyRepository deleteRepository,
        IGamesReadOnlyRepository readRepository,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _deleteRepository = deleteRepository;
        _readRepository = readRepository;
    }
    
    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var game = await _readRepository.GetById(loggedUser, id);
        if (game is null)
        {
            throw new NotFoundException(ResourceErrorMessages.GAME_NOT_FOUND);
        }
        await _deleteRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}