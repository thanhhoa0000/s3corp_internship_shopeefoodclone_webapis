namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<AdministrativeRegion> AdministrativeRegions { get; set; }
    public DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Ward> Wards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new StoreConfiguration());
        builder.ApplyConfiguration(new CollectionConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new SubCategoryConfiguration());
        builder.ApplyConfiguration(new ProvinceConfiguration());
        builder.ApplyConfiguration(new DistrictConfiguration());
        builder.ApplyConfiguration(new WardConfiguration());
    }
}
