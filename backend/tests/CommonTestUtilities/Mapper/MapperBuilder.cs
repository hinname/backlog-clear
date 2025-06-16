using AutoMapper;
using BacklogClear.Application.AutoMapper;

namespace CommonTestUtilities.Mapper;

public static class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });
        
        return mapper.CreateMapper();
    }
}