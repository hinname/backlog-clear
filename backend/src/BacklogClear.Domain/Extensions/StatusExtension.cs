using BacklogClear.Domain.Enums;
using BacklogClear.Domain.Reports;

namespace BacklogClear.Domain.Extensions;

public static class StatusExtension
{
    public static string StatusToString(this Status status)
    {
        return status switch
        {
            Status.Backlog => ResourceReportGenerationMessages.STATUS_BACKLOG,
            Status.Playing => ResourceReportGenerationMessages.STATUS_PLAYING,
            Status.Completed => ResourceReportGenerationMessages.STATUS_COMPLETED,
            Status.Dropped => ResourceReportGenerationMessages.STATUS_DROPPED,
            _ => string.Empty
        };
    }
}