using BacklogClear.Communication.Requests.Users;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(r => r.Email, faker => faker.Internet.Email())
            .RuleFor(r => r.Nickname, faker => faker.Internet.UserName())
            .RuleFor(r => r.Password, faker => faker.Internet.Password());
    }
}