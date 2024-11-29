using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IGamesRepository, GamesRepository>();
    }
}