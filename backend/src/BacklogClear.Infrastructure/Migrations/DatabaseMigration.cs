using BacklogClear.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogClear.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<BacklogClearDbContext>();
        await dbContext.Database.MigrateAsync();
        
        // Ensure the database is created and migrated
        // if (dbContext.Database.IsRelational())
        // {
        //     await dbContext.Database.EnsureDeletedAsync();
        //     await dbContext.Database.EnsureCreatedAsync();
        // }
        // else
        // {
        //     await dbContext.Database.EnsureCreatedAsync();
        // }
    }
}