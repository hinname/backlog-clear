namespace BacklogClear.Application.UseCases.Games.Register.Reports.Pdf;

public interface IGenerateGamesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}