using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Requests;
using FluentAssertions;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Games.Register;

public class RegisterGamesTest : BacklogClearClassFixture
{
    private const string METHOD = "api/game";
    private readonly string _token;

    public RegisterGamesTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterGameJsonBuilder.Build();
        
        var result = await DoPost(METHOD, request, token: _token);
        
        var body = await result.Content.ReadAsStreamAsync();
        
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ErrorTitleEmpty(string culture)
    {
        var request = RequestRegisterGameJsonBuilder.Build();
        request.Title = string.Empty;
        
        var result = await DoPost(METHOD, request, token: _token, culture: culture);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage =
            ResourceErrorMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(culture));
        errors.Should().HaveCount(1).And.Contain(errorMessage =>
            errorMessage.GetString()!.Equals(expectedMessage));
    }
}