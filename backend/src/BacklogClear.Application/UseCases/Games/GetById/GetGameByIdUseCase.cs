using AutoMapper;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Domain.Services.LoggedUser;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;

namespace BacklogClear.Application.UseCases.Games.GetById;

public class GetGameByIdUseCase : IGetGameByIdUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    
    public GetGameByIdUseCase(
        IGamesReadOnlyRepository repository, 
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }
    
    public async Task<ResponseGameJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repository.GetById(loggedUser, id);
        
        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.GAME_NOT_FOUND);
        }
        
        return _mapper.Map<ResponseGameJson>(result);
    }
}