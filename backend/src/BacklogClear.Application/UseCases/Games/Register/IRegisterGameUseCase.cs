using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;

namespace BacklogClear.Application.UseCases.Games.Register;

public interface IRegisterGameUseCase
{
    public ResponseRegisteredGameJson Execute(RequestRegisterGameJson request);
}