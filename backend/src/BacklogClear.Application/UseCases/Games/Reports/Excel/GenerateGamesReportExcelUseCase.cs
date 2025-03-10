using BacklogClear.Domain.Enums;
using BacklogClear.Domain.Reports;
using BacklogClear.Domain.Repositories.Games;
using ClosedXML.Excel;

namespace BacklogClear.Application.UseCases.Games.Register.Reports.Excel;

public class GenerateGamesReportExcelUseCase : IGenerateGamesReportExcelUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    public GenerateGamesReportExcelUseCase(IGamesReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var games = await _repository.FilterByReleaseDate(month);
        if (games.Count == 0)
            return [];
        
        using var workbook = new XLWorkbook();

        workbook.Author = "Test";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        
        var worksheet = workbook.Worksheets.Add($"{month.ToString("Y")} - Game Report");
        
        InsertHeader(worksheet);

        var row = 2;
        foreach (var game in games)
        {
            worksheet.Cell($"A{row}").Value = game.Title;
            worksheet.Cell($"B{row}").Value = game.Platform;
            
            worksheet.Cell($"C{row}").Value = game.ReleaseDate;
            worksheet.Cell($"C{row}").Style.NumberFormat.Format = "dd/MM/yyyy";
            
            worksheet.Cell($"D{row}").Value = ConvertStatusToString(game.Status);
            
            row++;
        }
        
        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }
    
    private string ConvertStatusToString(Status gameStatus)
    {
        return gameStatus switch
        {
            Status.Backlog => ResourceReportGenerationMessages.STATUS_BACKLOG,
            Status.Playing => ResourceReportGenerationMessages.STATUS_PLAYING,
            Status.Completed => ResourceReportGenerationMessages.STATUS_COMPLETED,
            Status.Dropped => ResourceReportGenerationMessages.STATUS_DROPPED,
            _ => string.Empty
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.GAME;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.PLATFORM;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.RELEASE_DATE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.STATUS;
        
        worksheet.Cells("A1:D1").Style.Fill.BackgroundColor = XLColor.LightGray;
        worksheet.Cells("A1:D1").Style.Font.Bold = true;

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}