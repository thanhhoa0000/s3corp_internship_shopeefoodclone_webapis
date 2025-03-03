namespace ShopeeFoodClone.WebApi.Users.Infrastructure.Persistence;

public class UserContext(DbContextOptions<UserContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>().ToTable("AppUsers");

        builder.Entity<AppRole>().ToTable("AppRoles");
    }
}