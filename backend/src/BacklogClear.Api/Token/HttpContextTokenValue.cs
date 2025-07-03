using BacklogClear.Domain.Security.Tokens;

namespace BacklogClear.Api.Token;

public class HttpContextTokenValue: ITokenProvider
{
    private readonly IHttpContextAccessor _httpContext;
    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor;
    }
    
    public string TokenOnRequest()
    {
        var authorization = _httpContext.HttpContext!.Request.Headers.Authorization.ToString();
        
        return authorization["Bearer ".Length..].Trim();
    }
}