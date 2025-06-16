using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Token;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(
            accessTokenGenerator => accessTokenGenerator.Generate(It.IsAny<User>()))
            .Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InN0cmluZyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3NpZCI6ImQ0OTk1ZDZhLTY5NmItNDVhZS04MzNhLWVjM2MwYTY1YmZhMCIsIm5iZiI6MTc1MDEwMjE0MiwiZXhwIjoxNzUwMTAyNzQyLCJpYXQiOjE3NTAxMDIxNDJ9.zxRnpA-euTEWLf1_g9NIj4_qJooBkOXRe2bzdsLsOME");
        
        return mock.Object;
    }
}