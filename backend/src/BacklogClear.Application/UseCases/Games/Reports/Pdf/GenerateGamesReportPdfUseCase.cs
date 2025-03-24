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

    public async Task<byte[]> Execute(DateOnly initialStartPlayingDate, DateOnly endingStartPlayingDate)
    {
        //Tratando a data inicial e final para DateTime
        var initialDate = initialStartPlayingDate.ToDateTime(new TimeOnly(
            hour: 0,
            minute: 0,
            second: 0
        ));
        var endingDate = endingStartPlayingDate.ToDateTime(new TimeOnly(
            hour: 23,
            minute: 59,
            second: 59
        ));
        
        var games = await _repository.FilterByStartPlayingDate(initialDate, endingDate);
        if (games.Count == 0)
            return [];
        
        var document = CreateDocument();
        var page = CreatePage(document, initialStartPlayingDate, endingStartPlayingDate);

         var paragraph = page.AddParagraph();
         

        return [];
    }
    
    private Document CreateDocument()
    {
        var document = new Document
        {
            Info =
            {
                Title = $"{ResourceReportGenerationMessages.GAME_REPORT}",
                Author = "Test"
            }
        };
        
        var styles = document.Styles["Normal"];
        styles!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }
    
    private Section CreatePage(Document document, DateOnly startDate, DateOnly endDate)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.Orientation = Orientation.Portrait;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        
        section.AddParagraph($"{ResourceReportGenerationMessages.GAME_REPORT} ({startDate.ToString("dd/MM/yyyy")} - {endDate.ToString("dd/MM/yyyy")})")
                .Format.Font.Size = 16;
        
        return section;
    }
}