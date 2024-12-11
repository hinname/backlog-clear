using BacklogClear.Communication.Enums;
using BacklogClear.Communication.Requests.Games;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestRegisterGameJsonBuilder
{
    public static RequestGameJson Build()
    {
        return new Faker<RequestGameJson>()
            .RuleFor(r => r.Title, faker => faker.Random.Words())
            .RuleFor(r => r.Platform, faker => faker.Random.Words())
            .RuleFor(r => r.Genre, faker => faker.Random.Words())
            .RuleFor(r => r.ReleaseDate, faker => faker.Date.Past())
            .RuleFor(r => r.Status, faker => faker.PickRandom<Status>());
    }
}