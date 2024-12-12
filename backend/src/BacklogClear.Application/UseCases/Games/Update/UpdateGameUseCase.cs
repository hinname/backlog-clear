using AutoMapper;
using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Communication.Requests.Games;
using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;

namespace BacklogClear.Application.UseCases.Games.Update;

public class UpdateGameUseCase : IUpdateGameUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGamesUpdateOnlyRepository _repository;
    public UpdateGameUseCase(IMapper mapper, IUnitOfWork unitOfWork, IGamesUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id, RequestGameJson request)
    {
        Validate(request);
        var game = await _repository.GetById(id);

        if (game is null)
        {
            throw new NotFoundException(ResourceErrorMessages.GAME_NOT_FOUND);
        }

        _mapper.Map(request, game);
        
        _repository.Update(game);
        
        await _unitOfWork.Commit();
    }
    
    private void Validate(RequestGameJson request)
    {
        var validator = new GameValidator();
        var result = validator.Validate(request);
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}