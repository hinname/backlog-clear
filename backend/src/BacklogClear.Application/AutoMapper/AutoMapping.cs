using AutoMapper;
using BacklogClear.Communication.Requests;
using BacklogClear.Communication.Responses;
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
        CreateMap<RequestRegisterGameJson, Game>();
    }
    
    private void EntityToResponse()
    {
        CreateMap<Game, ResponseRegisteredGameJson>();
    }
}