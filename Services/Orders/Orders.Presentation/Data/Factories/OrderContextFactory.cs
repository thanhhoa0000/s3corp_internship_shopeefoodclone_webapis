namespace ShopeeFoodClone.WebApi.Orders.Presentation.Data.Factories;

public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

        var connectionString = configuration.GetConnectionString("OrdersDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new OrderContext(optionsBuilder.Options);
    }
}