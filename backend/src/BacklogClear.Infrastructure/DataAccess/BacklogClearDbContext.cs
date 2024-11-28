using BacklogClear.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess;

public class BacklogClearDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=backlogcleardb;Uid=root;Pwd=mariadbroot123";
        var serverVersion = new MySqlServerVersion(new Version(11, 6, 2));
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}