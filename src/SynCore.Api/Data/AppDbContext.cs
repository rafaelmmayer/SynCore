using Microsoft.EntityFrameworkCore;
using SynCore.Core.Entities;

namespace SynCore.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Password> Passwords { get; set; }
}