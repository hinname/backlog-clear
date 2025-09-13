using System.Globalization;
using System.Net;
using System.Text.Json;
using BacklogClear.Communication.Enums;
using BacklogClear.Exception.Resources;
using FluentAssertions;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Games.GetById;

public class GetGameByIdTest : BacklogClearClassFixture
{
    private const string METHOD = "api/game";
    private readonly string _token;
    private readonly long _gameId;
    public GetGameByIdTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.GetToken();
        _gameId = factory.GetGameId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet($"{METHOD}/{_gameId}", token: _token);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.GetProperty("id").GetInt64().Should().Be(_gameId);
        response.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("platform").GetString().Should().NotBeNullOrWhiteSpace();
        
        var status = response.RootElement.GetProperty("status").GetInt32();
        Enum.IsDefined(typeof(Status), status).Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ErrorGameNotFound(string culture)
    {
        var result = await DoGet($"{METHOD}/{0}", token: _token, culture: culture);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage =
            ResourceErrorMessages.ResourceManager.GetString("GAME_NOT_FOUND", new CultureInfo(culture));
        errors.Should().HaveCount(1).And.Contain(errorMessage =>
            errorMessage.GetString()!.Equals(expectedMessage));
        
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}