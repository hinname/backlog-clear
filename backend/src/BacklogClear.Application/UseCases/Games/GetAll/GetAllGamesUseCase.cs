using AutoMapper;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Domain.Services.LoggedUser;

namespace BacklogClear.Application.UseCases.Games.GetAll;

public class GetAllGamesUseCase : IGetAllGamesUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetAllGamesUseCase(
        IGamesReadOnlyRepository repository, 
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }
    
    public async Task<ResponseGamesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repository.GetAll(loggedUser);

        return new ResponseGamesJson()
        {
            Games = _mapper.Map<List<ResponseShortGameJson>>(result)
        };
    }
}