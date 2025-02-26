using ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence.Configurations;

namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>().ToTable("AppUsers");

        builder.Entity<AppRole>().ToTable("AppRoles");

        builder.ApplyConfiguration(new AppRoleConfiguration());
        builder.ApplyConfiguration(new RefreshTokenConfiguration());
    }
}