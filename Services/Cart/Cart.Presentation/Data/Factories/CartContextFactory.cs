namespace ShopeeFoodClone.WebApi.Cart.Presentation.Data.Factories;

public class CartContextFactory : IDesignTimeDbContextFactory<CartContext>
{
    public CartContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CartContext>();

        var connectionString = configuration.GetConnectionString("CartDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new CartContext(optionsBuilder.Options);
    }
}
