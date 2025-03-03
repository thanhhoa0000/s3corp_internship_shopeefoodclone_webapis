namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Store>()
            .HasMany(s => s.Categories)
            .WithMany(c => c.Stores);

        builder.ApplyConfiguration(new CategoryConfiguration());
    }
}