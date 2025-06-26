using System.Net;
using System.Net.Http.Json;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Tests.Users.Register;

public class RegisterUserTest: IClassFixture<WebApplicationFactory<Program>>
{
    private const string METHOD = "api/user";
    
    private readonly HttpClient _httpClient;

    public RegisterUserTest(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

    }
}