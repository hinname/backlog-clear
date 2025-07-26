using BacklogClear.Domain.Entities;
using BacklogClear.Domain.Enums;
using Bogus;

namespace CommonTestUtilities.Entities;

public class GameBuilder
{
    public static List<Game> Collection(User user, int count = 3)
    {
        var games = new List<Game>();

        if (count == 0)
            count = 1;
        
        var gameId = 1;
        for (var i = 0; i < count; i++)
        {
            var game = Build(user);
            game.Id = gameId++;
            
            games.Add(game);
        }

        return games;
    }

    public static Game Build(User user)
    {
        var game = new Faker<Game>()
            .RuleFor(g => g.Id, _ => 1)
            .RuleFor(g => g.Title, faker => faker.Random.Words())
            .RuleFor(g => g.Platform, faker => faker.Random.Words())
            .RuleFor(g => g.Genre, faker => faker.Random.Words())
            .RuleFor(g => g.ReleaseDate, faker => faker.Date.Past())
            .RuleFor(g => g.Status, faker => faker.PickRandom<Status>())
            .RuleFor(g => g.UserId, faker => user.Id);
        
        return game;
    }
}