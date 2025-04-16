namespace ShopeeFoodClone.WebApi.Cart.Infrastructure.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(i => i.CartHeader)
            .WithMany()
            .HasForeignKey(i => i.CartHeaderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}