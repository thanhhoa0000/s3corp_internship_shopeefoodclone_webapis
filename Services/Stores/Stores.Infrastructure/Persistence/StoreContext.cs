namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<StoreAddress> StoreAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new StoreConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new StoreAddressConfiguration());
    }
}