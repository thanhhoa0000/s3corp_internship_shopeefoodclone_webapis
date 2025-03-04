namespace ShopeeFoodClone.WebApi.Products.Presentation.Data.Factories;

public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
{
    public ProductContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();

        var connectionString = configuration.GetConnectionString("ProductsDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new ProductContext(optionsBuilder.Options);
    }
}
