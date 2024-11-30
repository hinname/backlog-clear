using BacklogClear.Application.UseCases.Games.Register;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Application;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterGameUseCase, RegisterGameUseCase>();
    }
}