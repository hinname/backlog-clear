namespace BacklogClear.Application.UseCases.Games.Register.Reports.Pdf;

public class GenerateGamesReportPdfUseCase : IGenerateGamesReportPdfUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    public GenerateGamesReportPdfUseCase(IGamesReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var games = await _repository.FilterByReleaseDate(month);
        if (games.Count == 0)
            return [];

        return [];
    }
}