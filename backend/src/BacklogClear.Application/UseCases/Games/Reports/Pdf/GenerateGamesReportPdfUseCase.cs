using BacklogClear.Application.UseCases.Games.Reports.Pdf.Fonts;
using BacklogClear.Domain.Reports;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace BacklogClear.Application.UseCases.Games.Register.Reports.Pdf;

public class GenerateGamesReportPdfUseCase : IGenerateGamesReportPdfUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    public GenerateGamesReportPdfUseCase(IGamesReadOnlyRepository repository)
    {
        _repository = repository;
        GlobalFontSettings.FontResolver = new GamesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var games = await _repository.FilterByReleaseDate(month);
        if (games.Count == 0)
            return [];
        
        var document = CreateDocument(month);
        var page = CreatePage(document, month);

        return [];
    }
    
    private Document CreateDocument(DateOnly month)
    {
        var document = new Document
        {
            Info =
            {
                Title = $"{month.ToString("Y")} - {ResourceReportGenerationMessages.GAME_REPORT}",
                Author = "Test"
            }
        };
        
        var styles = document.Styles["Normal"];
        styles!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }
    
    private Section CreatePage(Document document, DateOnly month)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.Orientation = Orientation.Portrait;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        
        section.AddParagraph($"{month.ToString("Y")} - {ResourceReportGenerationMessages.GAME_REPORT}")
            .Format.Font.Size = 16;
        
        return section;
    }
}