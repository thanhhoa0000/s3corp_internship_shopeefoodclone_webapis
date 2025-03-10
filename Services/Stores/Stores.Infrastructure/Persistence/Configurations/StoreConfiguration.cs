namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasMany(s => s.Categories)
            .WithMany(c => c.Stores);
        
        builder
            .HasOne(s => s.Ward)
            .WithMany()
            .HasForeignKey(s => s.WardCode)
            .OnDelete(DeleteBehavior.SetNull);
    }
}