namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(c => c.Name).IsUnique();
        builder.HasIndex(c => c.CodeName).IsUnique();
    }
}