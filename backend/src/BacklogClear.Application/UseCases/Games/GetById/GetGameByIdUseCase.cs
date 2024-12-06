using AutoMapper;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Repositories.Games;

namespace BacklogClear.Application.UseCases.Games.GetById;

public class GetGameByIdUseCase : IGetGameByIdUseCase
{
    private readonly IGamesRepository _repository;
    private readonly IMapper _mapper;
    
    public GetGameByIdUseCase(IGamesRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ResponseGameJson?> Execute(long id)
    {
        var result = await _repository.GetById(id);
        return _mapper.Map<ResponseGameJson>(result);
    }
}