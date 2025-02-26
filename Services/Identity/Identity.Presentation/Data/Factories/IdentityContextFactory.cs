namespace ShopeeFoodClone.WebApi.Identity.Presentation.Data.Factories;

public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

        var connectionString = configuration.GetConnectionString("UsersDB");

        optionsBuilder.UseSqlServer(connectionString);

        return new IdentityContext(optionsBuilder.Options);
    }
}