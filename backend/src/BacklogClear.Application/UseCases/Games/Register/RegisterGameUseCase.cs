using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;

namespace BacklogClear.Application.UseCases.Games.Register;

public class RegisterGameUseCase
{
    public ResponseRegisteredGameJson Execute(RequestRegisterGameJson request)
    {
        return new ResponseRegisteredGameJson();
    }
}