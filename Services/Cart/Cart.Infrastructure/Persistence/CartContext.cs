using ShopeeFoodClone.WebApi.Cart.Infrastructure.Persistence.Configurations;

namespace ShopeeFoodClone.WebApi.Cart.Infrastructure.Persistence;

public class CartContext(DbContextOptions<CartContext> options) : DbContext(options)
{
    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CartItemConfiguration());
    }
}