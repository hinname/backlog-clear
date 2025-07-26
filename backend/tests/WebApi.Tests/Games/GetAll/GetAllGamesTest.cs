using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Tests.Games.GetAll;

public class GetAllGamesTest : BacklogClearClassFixture
{
    private const string METHOD = "api/game";
    private readonly string _token;
    public GetAllGamesTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.GetToken();
    }
    
    [Fact]
    public async Task Success()
    {
        var result = await DoGet(METHOD, token: _token);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("games").EnumerateArray().Should().NotBeNullOrEmpty();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Unauthorized()
    {
        var result = await DoGet(METHOD);
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
}