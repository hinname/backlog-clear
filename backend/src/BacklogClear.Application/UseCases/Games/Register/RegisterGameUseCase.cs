using AutoMapper;
using BacklogClear.Communication.Requests.Games;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Exception.ExceptionBase;

namespace BacklogClear.Application.UseCases.Games.Register;

public class RegisterGameUseCase : IRegisterGameUseCase
{
    private readonly IGamesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterGameUseCase(IGamesWriteOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ResponseRegisteredGameJson> Execute(RequestGameJson request)
    {
        Validate(request);
        var entity = _mapper.Map<Game>(request);
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        return _mapper.Map<ResponseRegisteredGameJson>(entity);
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