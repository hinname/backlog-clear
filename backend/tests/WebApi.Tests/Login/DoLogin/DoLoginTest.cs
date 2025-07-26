using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using BacklogClear.Communication.Requests.Login;
using BacklogClear.Exception.Resources;
using FluentAssertions;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Login.DoLogin;

public class DoLoginTest : BacklogClearClassFixture
{
    private const string METHOD = "api/login";

    private readonly string _email;
    private readonly string _password;

    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _email = factory.GetEmail();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson()
        {
            Email = _email,
            Password = _password
        };

        var result = await DoPost(METHOD, request);
        
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        response.RootElement.GetProperty("email").GetString().Should().Be(request.Email);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Email_Language_Middleware(string language)
    {
        var request = new RequestLoginJson()
        {
            Email = _email,
            Password = _password
        };
        request.Email = string.Empty;

        var result = await DoPost(METHOD, request, culture: language);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("USER_EMAIL_REQUIRED", new CultureInfo(language));
        errors.Should().HaveCount(1).And.Contain(errorMessage => 
            errorMessage.GetString()!.Equals(expectedMessage));
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Error_Not_Matching_Password()
    {
        var request = new RequestLoginJson()
        {
            Email = _email,
            Password = "StarWithoutNumber123@"
        };

        var result = await DoPost(METHOD, request);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Should().HaveCount(1).And.Contain(errorMessage => 
            errorMessage.GetString()!.Equals(ResourceErrorMessages.LOGIN_EMAIL_OR_PASSWORD_INVALID));
        
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        var request = new RequestLoginJson()
        {
            Email = "teste@test.com.br",
            Password = _password
        };

        var result = await DoPost(METHOD, request);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);
        
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Should().HaveCount(1).And.Contain(errorMessage => 
            errorMessage.GetString()!.Equals(ResourceErrorMessages.LOGIN_EMAIL_OR_PASSWORD_INVALID));
        
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}