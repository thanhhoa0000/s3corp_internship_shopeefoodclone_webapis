namespace ShopeeFoodClone.WebApi.Payment.Presentation.Data.Factories;

public class PaymentContextFactory : IDesignTimeDbContextFactory<PaymentContext>
{
    public PaymentContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();

        var connectionString = configuration.GetConnectionString("PaymentDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new PaymentContext(optionsBuilder.Options);
    }
}