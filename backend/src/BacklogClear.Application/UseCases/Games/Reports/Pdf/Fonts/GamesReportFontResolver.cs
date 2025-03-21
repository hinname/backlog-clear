using PdfSharp.Fonts;

namespace BacklogClear.Application.UseCases.Games.Reports.Pdf.Fonts;

public class GamesReportFontResolver : IFontResolver
{

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }
    public byte[]? GetFont(string faceName)
    {
        throw new NotImplementedException();
    }
}