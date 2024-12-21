namespace BacklogClear.Application.UseCases.Games.Register.Reports.Excel;

public interface IGenerateGamesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}