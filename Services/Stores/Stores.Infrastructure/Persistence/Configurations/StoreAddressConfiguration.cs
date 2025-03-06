namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class StoreAddressConfiguration : IEntityTypeConfiguration<StoreAddress>
{
    public void Configure(EntityTypeBuilder<StoreAddress> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
