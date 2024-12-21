using System.Net.Mime;
using BacklogClear.Application.UseCases.Games.Register.Reports.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(typeof(FileContentResult),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel(
            [FromServices] IGenerateGamesReportExcelUseCase useCase,
            [FromHeader] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);
            
            if (file.Length == 0)
                return NoContent();
            
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }
    }
}
