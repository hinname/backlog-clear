using AutoMapper;
using BacklogClear.Communication.Requests.Games;
using BacklogClear.Communication.Requests.Users;
using BacklogClear.Communication.Responses.Games;
using BacklogClear.Domain.Entities;

namespace BacklogClear.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }
    
    private void RequestToEntity()
    {
        CreateMap<RequestGameJson, Game>();
        CreateMap<RequestRegisterUserJson, User>();
    }
    
    private void EntityToResponse()
    {
        CreateMap<Game, ResponseRegisteredGameJson>();
        CreateMap<Game, ResponseShortGameJson>();
        CreateMap<Game, ResponseGameJson>();
    }
}