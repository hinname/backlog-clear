using BacklogClear.Application.AutoMapper;
using BacklogClear.Application.UseCases.Games.Delete;
using BacklogClear.Application.UseCases.Games.GetAll;
using BacklogClear.Application.UseCases.Games.GetById;
using BacklogClear.Application.UseCases.Games.Register;
using BacklogClear.Application.UseCases.Games.Register.Reports.Excel;
using BacklogClear.Application.UseCases.Games.Register.Reports.Pdf;
using BacklogClear.Application.UseCases.Games.Update;
using BacklogClear.Application.UseCases.Users.Register;
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
        #region games
        services.AddScoped<IRegisterGameUseCase, RegisterGameUseCase>();
        services.AddScoped<IGetAllGamesUseCase, GetAllGamesUseCase>();
        services.AddScoped<IGetGameByIdUseCase, GetGameByIdUseCase>();
        services.AddScoped<IDeleteGameUseCase, DeleteGameUseCase>();
        services.AddScoped<IUpdateGameUseCase, UpdateGameUseCase>();
        services.AddScoped<IGenerateGamesReportExcelUseCase, GenerateGamesReportExcelUseCase>();
        services.AddScoped<IGenerateGamesReportPdfUseCase, GenerateGamesReportPdfUseCase>();
        #endregion
        
        #region users
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        #endregion
    }
}