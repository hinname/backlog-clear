using BacklogClear.Application.AutoMapper;
using BacklogClear.Application.UseCases.Games.Delete;
using BacklogClear.Application.UseCases.Games.GetAll;
using BacklogClear.Application.UseCases.Games.GetById;
using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Application.UseCases.Games.Register.Reports.Excel;
using BacklogClear.Application.UseCases.Games.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Application;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }
    
    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
    
    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterGameUseCase, RegisterGameUseCase>();
        services.AddScoped<IGetAllGamesUseCase, GetAllGamesUseCase>();
        services.AddScoped<IGetGameByIdUseCase, GetGameByIdUseCase>();
        services.AddScoped<IDeleteGameUseCase, DeleteGameUseCase>();
        services.AddScoped<IUpdateGameUseCase, UpdateGameUseCase>();
        services.AddScoped<IGenerateGamesReportExcelUseCase, GenerateGamesReportExcelUseCase>();
    }
}