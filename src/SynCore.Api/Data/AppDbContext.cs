using Microsoft.EntityFrameworkCore;
using SynCore.Api.Services;
using SynCore.Core.Entities;

namespace SynCore.Api.Data;

public class AppDbContext : DbContext
{
    private readonly Guid? _userId;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, AuthUserProvider authUserProvider)
        : base(options)
    {
        _userId = authUserProvider.GetAuthUser()?.Id;
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Password> Passwords { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassTime> ClassTimes { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(builder =>
        {
            builder.HasIndex(c => c.UserId);
            
            builder.HasQueryFilter(c => c.UserId == _userId);
        });
    }
}