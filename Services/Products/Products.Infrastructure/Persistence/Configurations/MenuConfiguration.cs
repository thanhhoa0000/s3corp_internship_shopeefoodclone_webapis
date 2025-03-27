namespace ShopeeFoodClone.WebApi.Products.Infrastructure.Persistence.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasMany(s => s.Products)
            .WithMany(c => c.Menus);
    }
}
