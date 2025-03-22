using System.Reflection;
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
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelper.DEFAULT_FONT);

        var length = (int)stream!.Length;
        var fontData = new byte[length];
        
        stream.Read(fontData, 0, length);
        
        return fontData;
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly(); //referencia a dll atual (BacklogClear.Application)

        return assembly.GetManifestResourceStream($"BacklogClear.Application.UseCases.Games.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}