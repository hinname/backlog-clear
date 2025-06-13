using BacklogClear.Communication.Requests.Users;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(requestUser => requestUser.Name, faker => faker.Internet.UserName())
            .RuleFor(requestUser => requestUser.Email, (faker, requestUser) => faker.Internet.Email(requestUser.Name))
            .RuleFor(requestUser => requestUser.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}