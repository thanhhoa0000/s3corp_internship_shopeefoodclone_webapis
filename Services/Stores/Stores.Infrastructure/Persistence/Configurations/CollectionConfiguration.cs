namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(c => c.Stores)
            .WithMany(c => c.Collections);
    }
}
