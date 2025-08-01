using BacklogClear.Domain.Enums;
using BacklogClear.Domain.Extensions;
using BacklogClear.Domain.Reports;
using BacklogClear.Domain.Repositories.Games;
using BacklogClear.Domain.Services.LoggedUser;
using ClosedXML.Excel;

namespace BacklogClear.Application.UseCases.Games.Register.Reports.Excel;

public class GenerateGamesReportExcelUseCase : IGenerateGamesReportExcelUseCase
{
    private readonly IGamesReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    public GenerateGamesReportExcelUseCase(IGamesReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
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
        
        using var workbook = new XLWorkbook();

        workbook.Author = loggedUser.Name;
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        
        var worksheet = workbook.Worksheets.Add($"{DateTime.Now:Y} - {ResourceReportGenerationMessages.GAME_REPORT}");
        
        InsertHeader(worksheet);

        var row = 2;
        foreach (var game in games)
        {
            worksheet.Cell($"A{row}").Value = game.Title;
            worksheet.Cell($"B{row}").Value = game.Platform;
            
            worksheet.Cell($"C{row}").Value = game.StartPlayingDate;
            worksheet.Cell($"C{row}").Style.NumberFormat.Format = "dd/MM/yyyy";

            if (game.EndPlayingDate is null)
            {
                worksheet.Cell($"D{row}").Value = ResourceReportGenerationMessages.NOT_FINISHED;
            }
            else
            {
                worksheet.Cell($"D{row}").Value = game.EndPlayingDate ;
                worksheet.Cell($"D{row}").Style.NumberFormat.Format = "dd/MM/yyyy";
            }
            
            worksheet.Cell($"E{row}").Value = game.ReleaseDate;
            worksheet.Cell($"E{row}").Style.NumberFormat.Format = "dd/MM/yyyy";
            
            worksheet.Cell($"F{row}").Value = game.Status.StatusToString();
            
            row++;
        }
        
        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.GAME;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.PLATFORM;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.STARTED_PLAYING_DATE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.FINISHED_PLAYING_DATE;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.RELEASE_DATE;
        worksheet.Cell("F1").Value = ResourceReportGenerationMessages.STATUS;
        
        worksheet.Cells("A1:F1").Style.Fill.BackgroundColor = XLColor.LightGray;
        worksheet.Cells("A1:F1").Style.Font.Bold = true;

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("F1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}