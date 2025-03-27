using BacklogClear.Application.UseCases.Games.Reports.Pdf.Fonts;
using BacklogClear.Domain.Reports;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
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
        
        var paragraphHeader = page.AddParagraph();
        paragraphHeader.AddFormattedText("Hey, User!", new Font{ Name = FontHelper.RALEWAY_BLACK, Size = 16});
        
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = 40;
        paragraph.Format.SpaceAfter = 40;
        var title = string.Format(
            ResourceReportGenerationMessages.TOTAL_PLAYED_IN,
            initialStartPlayingDate.ToString("dd/MM/yyyy"),
            endingStartPlayingDate.ToString("dd/MM/yyyy")
            ); 
        paragraph.AddFormattedText(title, new Font{ Name = FontHelper.RALEWAY_REGULAR, Size = 14});
        
        paragraph.AddLineBreak();
        
        paragraph.AddFormattedText(games.Count.ToString(), new Font{ Name = FontHelper.WORKSANS_BLACK, Size = 50});

        return RenderDocument(document);
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
    
    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer(true)
        {
            Document = document
        };
        renderer.RenderDocument();
        
        using var stream = new MemoryStream();
        renderer.PdfDocument.Save(stream, false);
        
        return stream.ToArray();
    }
}