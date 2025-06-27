using BacklogClear.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess;

public class BacklogClearDbContext : DbContext
{
    public BacklogClearDbContext(DbContextOptions options) : base(options){}
    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
}