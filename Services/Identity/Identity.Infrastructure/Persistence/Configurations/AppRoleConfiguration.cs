namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence.Configurations;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(r => r.Description)
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasData(
            new AppRole
            {
                Id = Guid.NewGuid(), 
                Name = "Admin", 
                NormalizedName = "ADMIN", 
                Description = "Administrator role",
                ConcurrencyStamp = Guid.Empty.ToString()
            },
            new AppRole
            {
                Id = Guid.NewGuid(), 
                Name = "Customer", 
                NormalizedName = "CUSTOMER", 
                Description = "Customer role",
                ConcurrencyStamp = Guid.Empty.ToString()
            },
            new AppRole
            {
                Id = Guid.NewGuid(), 
                Name = "Vendor", 
                NormalizedName = "VENDOR", 
                Description = "Vendor role",
                ConcurrencyStamp = Guid.Empty.ToString()
            }
        );
    }
}