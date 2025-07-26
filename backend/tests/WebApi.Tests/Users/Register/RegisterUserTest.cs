using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using BacklogClear.Exception.Resources;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : BacklogClearClassFixture(factory)
{
    private const string METHOD = "api/user";
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await DoPost(METHOD, request);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.GetProperty("email").GetString().Should().Be(request.Email);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Error_Empty_Name()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPost(METHOD, request);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Should().HaveCount(1).And.Contain(errorMessage => 
            errorMessage.GetString()!.Equals(ResourceErrorMessages.USER_NAME_REQUIRED));
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Error_Empty_Email()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var result = await DoPost(METHOD, request);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Should().HaveCount(1).And.Contain(errorMessage => 
            errorMessage.GetString()!.Equals(ResourceErrorMessages.USER_EMAIL_REQUIRED));
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name_Language_Middleware(string language)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPost(METHOD, request, culture: language);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("USER_NAME_REQUIRED", new CultureInfo(language));
        errors.Should().HaveCount(1).And.Contain(errorMessage => 
            errorMessage.GetString()!.Equals(expectedMessage));
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}