using BacklogClear.Application.UseCases.Games.Register.Reports.Pdf.Colors;
using BacklogClear.Application.UseCases.Games.Reports.Pdf.Fonts;
using BacklogClear.Domain.Extensions;
using BacklogClear.Domain.Reports;
using BacklogClear.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace BacklogClear.Application.UseCases.Games.Register.Reports.Pdf;

public class GenerateGamesReportPdfUseCase : IGenerateGamesReportPdfUseCase
{
    private const int HEIGHT_ROW_GAMES_TABLE = 16;
    private readonly IGamesReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    public GenerateGamesReportPdfUseCase(IGamesReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        GlobalFontSettings.FontResolver = new GamesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly initialStartPlayingDate, DateOnly endingStartPlayingDate)
    {
        var loggedUser = await _loggedUser.Get();
            
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
        
        var games = await _repository.FilterByStartPlayingDate(initialDate, endingDate, loggedUser);
        if (games.Count == 0)
            return [];
        
        var document = CreateDocument(loggedUser.Name);
        var page = CreatePage(document, initialStartPlayingDate, endingStartPlayingDate);
        var totalGames = games.Count;
        CreateHeader(page, initialStartPlayingDate, endingStartPlayingDate, totalGames, loggedUser.Name);

        foreach (var game in games)
        {
            var table = CreateTableGames(page);
            
            var row = table.AddRow();
            row.Borders.Visible = false;
            row.Height = HEIGHT_ROW_GAMES_TABLE;
            
            AddGameTitle(row.Cells[0], game.Title);
            AddHeaderForGameStatus(row.Cells[3]);
            
            row = table.AddRow();
            row.Borders.Visible = false;
            row.Height = HEIGHT_ROW_GAMES_TABLE;
            
            row.Cells[0].AddParagraph(game.StartPlayingDate?.ToString("D") ?? string.Empty);
            SetStyleBaseForGameInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[1].AddParagraph(game.Platform);
            SetStyleBaseForGameInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(game.Genre);
            SetStyleBaseForGameInformation(row.Cells[2]);

            AddGameStatus(row.Cells[3], game.Status.StatusToString());
            
            AddWhiteSpace(table);

        }

        return RenderDocument(document);
    }
    
    private Document CreateDocument(string author)
    {
        var document = new Document
        {
            Info =
            {
                Title = $"{ResourceReportGenerationMessages.GAME_REPORT}",
                Author = author
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
    private void CreateHeader(Section page, DateOnly startDate, DateOnly endDate, int totalGames, string name)
    {
        
        page.AddParagraph($"{ResourceReportGenerationMessages.GAME_REPORT} ({startDate.ToString("dd/MM/yyyy")} - {endDate.ToString("dd/MM/yyyy")})")
                        .Format.Font.Size = 16;
        
        var paragraphHeader = page.AddParagraph();
        paragraphHeader.AddFormattedText($"Hey, {name}!", new Font{ Name = FontHelper.RALEWAY_BLACK, Size = 16});
        
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

        table.Borders.Visible = false;
        table.LeftPadding = 0;
        table.RightPadding = 0;

        return table;
    }
    private void AddGameTitle(Cell cell, string gameTitle)
    {
        cell.AddParagraph(gameTitle);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Format.Shading.Color = ColorsHelper.RED_LIGHT;
        cell.Format.Alignment = ParagraphAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }
    private void AddHeaderForGameStatus(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.STATUS);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Format.Shading.Color = ColorsHelper.RED_DARK;
        cell.Format.Alignment = ParagraphAlignment.Center;
    }
    private void SetStyleBaseForGameInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Format.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.Format.Alignment = ParagraphAlignment.Center;
    }
    private void AddGameStatus(Cell cell, string gameStatus)
    {
        
        cell.AddParagraph(gameStatus);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Format.Shading.Color = ColorsHelper.WHITE;
        cell.Format.Alignment = ParagraphAlignment.Center;
    }
    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
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