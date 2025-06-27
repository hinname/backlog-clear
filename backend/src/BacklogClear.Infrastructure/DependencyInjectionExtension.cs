using BacklogClear.Domain.Repositories;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Domain.Repositories.Users;
using BacklogClear.Domain.Security.Crytography;
using BacklogClear.Domain.Security.Tokens;
using BacklogClear.Infrastructure.DataAccess;
using BacklogClear.Infrastructure.DataAccess.Repositories;
using BacklogClear.Infrastructure.Extensions;
using BacklogClear.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddSecurity(services);
        AddToken(services, configuration);

        if (!configuration.IsTestEnvironment())
        {
            AddDbContext(services, configuration);
        }
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGamesWriteOnlyRepository, GamesRepository>();
        services.AddScoped<IGamesReadOnlyRepository, GamesRepository>();
        services.AddScoped<IGamesDeleteOnlyRepository, GamesRepository>();
        services.AddScoped<IGamesUpdateOnlyRepository, GamesRepository>();
        
        services.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
        services.AddScoped<IUsersWriteOnlyRepository, UsersRepository>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");;
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        
        services.AddDbContext<BacklogClearDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
    private static void AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTime = configuration.GetValue<uint>("Settings:Jwt:ExpiresInMinutes");
        var signinKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTime, signinKey!));
    }
}