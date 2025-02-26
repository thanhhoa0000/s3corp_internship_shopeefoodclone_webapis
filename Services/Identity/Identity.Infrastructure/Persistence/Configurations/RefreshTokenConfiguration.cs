namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token).HasMaxLength(200);
        
        builder.HasIndex(r => r.Token).IsUnique();

        builder.HasOne(r => r.AppUser).WithMany().HasForeignKey(r => r.AppUserId).IsRequired();
    }
}