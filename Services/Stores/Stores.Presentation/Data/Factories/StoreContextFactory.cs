namespace ShopeeFoodClone.WebApi.Stores.Presentation.Data.Factories;

public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
{
    public StoreContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

        var connectionString = configuration.GetConnectionString("StoresDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new StoreContext(optionsBuilder.Options);
    }
}
