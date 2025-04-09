using BacklogClear.Application.UseCases.Games.Register.Reports.Pdf.Colors;
using BacklogClear.Application.UseCases.Games.Reports.Pdf.Fonts;
using BacklogClear.Domain.Reports;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
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
        var totalGames = games.Count;
        CreateHeader(page, initialStartPlayingDate, endingStartPlayingDate, totalGames);

        foreach (var game in games)
        {
            var table = CreateTableGames(page);
            var row = table.AddRow();
            row.Height = 25;
            
            row.Cells[0].AddParagraph(game.Title);
            row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
            row.Cells[0].Format.Shading.Color = ColorsHelper.RED_LIGHT;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].MergeRight = 2;
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[3].AddParagraph(ResourceReportGenerationMessages.STATUS);
            row.Cells[3].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
            row.Cells[3].Format.Shading.Color = ColorsHelper.RED_DARK;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;

            row = table.AddRow();
            row.Height = 25;
            
            row.Cells[0].AddParagraph(game.StartPlayingDate?.ToString("D") ?? string.Empty);
            row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[0].Format.Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[1].AddParagraph(game.Platform);
            row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[1].Format.Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;

            row.Cells[2].AddParagraph(game.Genre);
            row.Cells[2].Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[2].Format.Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            

            row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;

        }

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
        
        return section;
    }
    private void CreateHeader(Section page, DateOnly startDate, DateOnly endDate, int totalGames)
    {
        
        page.AddParagraph($"{ResourceReportGenerationMessages.GAME_REPORT} ({startDate.ToString("dd/MM/yyyy")} - {endDate.ToString("dd/MM/yyyy")})")
                        .Format.Font.Size = 16;
        
        var paragraphHeader = page.AddParagraph();
        paragraphHeader.AddFormattedText("Hey, User!", new Font{ Name = FontHelper.RALEWAY_BLACK, Size = 16});
        
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = 40;
        paragraph.Format.SpaceAfter = 40;
        var title = string.Format(
            ResourceReportGenerationMessages.TOTAL_PLAYED_IN,
            startDate.ToString("dd/MM/yyyy"),
            endDate.ToString("dd/MM/yyyy")
        ); 
        paragraph.AddFormattedText(title, new Font{ Name = FontHelper.RALEWAY_REGULAR, Size = 14});
        
        paragraph.AddLineBreak();
        
        paragraph.AddFormattedText(totalGames.ToString(), new Font{ Name = FontHelper.WORKSANS_BLACK, Size = 50});
    }
    private Table CreateTableGames(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("180").Format.Alignment = ParagraphAlignment.Left; //startPlayingDate complete
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center; //Platform
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center; //Genre
        table.AddColumn("95").Format.Alignment = ParagraphAlignment.Center; //Status

        return table;
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