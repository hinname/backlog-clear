using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Infrastructure.DataAccess;
using BacklogClear.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddRepositories(services);
        AddDbContext(services);
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IGamesRepository, GamesRepository>();
    }
    private static void AddDbContext(IServiceCollection services)
    {
        services.AddDbContext<BacklogClearDbContext>();
    }
}