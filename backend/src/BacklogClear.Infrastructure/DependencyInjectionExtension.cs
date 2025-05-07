using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Domain.Repositories.Users;
using BacklogClear.Domain.Security.Crytography;
using BacklogClear.Infrastructure.DataAccess;
using BacklogClear.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddSecurity(services);
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGamesWriteOnlyRepository, GamesRepository>();
        services.AddScoped<IGamesReadOnlyRepository, GamesRepository>();
        services.AddScoped<IGamesDeleteOnlyRepository, GamesRepository>();
        services.AddScoped<IGamesUpdateOnlyRepository, GamesRepository>();
        
        services.AddScoped<IUserReadOnlyRepository, UsersRepository>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");;
        var serverVersion = new MySqlServerVersion(new Version(11, 6, 2));
        
        services.AddDbContext<BacklogClearDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
    private static void AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, Security.BCrypt>();
    }
}