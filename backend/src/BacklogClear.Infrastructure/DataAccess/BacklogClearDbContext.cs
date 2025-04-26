using BacklogClear.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BacklogClear.Infrastructure.DataAccess;

internal class BacklogClearDbContext : DbContext
{
    public BacklogClearDbContext(DbContextOptions options) : base(options){}
    public DbSet<Game> Games { get; set; }
}