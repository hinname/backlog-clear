using AutoMapper;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Exception.ExceptionBase;
using BacklogClear.Exception.Resources;

namespace BacklogClear.Application.UseCases.Games.GetById;

public class GetGameByIdUseCase : IGetGameByIdUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    
    public GetGameByIdUseCase(IGamesReadOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ResponseGameJson> Execute(long id)
    {
        var result = await _repository.GetById(id);
        
        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.GAME_NOT_FOUND);
        }
        
        return _mapper.Map<ResponseGameJson>(result);
    }
}