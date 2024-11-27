using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestRegisterGameJsonBuilder
{
    public static RequestRegisterGameJson Build()
    {
        return new Faker<RequestRegisterGameJson>()
            .RuleFor(r => r.Title, faker => faker.Random.Words())
            .RuleFor(r => r.Platform, faker => faker.Random.Words())
            .RuleFor(r => r.Genre, faker => faker.Random.Words())
            .RuleFor(r => r.ReleaseDate, faker => faker.Date.Past())
            .RuleFor(r => r.Status, faker => faker.PickRandom<Status>());
    }
}