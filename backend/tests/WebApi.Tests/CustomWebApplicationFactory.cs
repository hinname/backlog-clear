using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Security.Crytography;
using BacklogClear.Domain.Security.Tokens;
using BacklogClear.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private User _user;
    private string _password;
    private string _token;

    public string GetEmail()
    {
       return _user.Email;
    }

    public string GetPassword()
    {
        return _password;
    }
    public string GetToken()
    {
        return _token;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                
                services.AddDbContext<BacklogClearDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDatabase");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BacklogClearDbContext>();
                var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                
                
                StartDatabase(dbContext, passwordEncrypter);

                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();
                _token = tokenGenerator.Generate(_user);
            });
    }

    private void StartDatabase(BacklogClearDbContext dbContext, IPasswordEncrypter passwordEncrypter)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;
        _user.Password = passwordEncrypter.Encrypt(_user.Password);
        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}