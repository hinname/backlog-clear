using AutoMapper;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;

namespace BacklogClear.Application.UseCases.Games.GetAll;

public class GetAllGamesUseCase : IGetAllGamesUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetAllGamesUseCase(IGamesReadOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ResponseGamesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseGamesJson()
        {
            Games = _mapper.Map<List<ResponseShortGameJson>>(result)
        };
    }
}