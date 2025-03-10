namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(x => x.Code);
        
        builder.HasOne(p => p.AdministrativeRegion)
            .WithMany()
            .HasForeignKey(p => p.AdministrativeRegionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("provinces_administrative_region_id_fkey");
        
        builder.HasOne(p => p.AdministrativeUnit)
            .WithMany()
            .HasForeignKey(p => p.AdministrativeUnitId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("provinces_administrative_unit_id_fkey");

        builder.HasIndex(p => p.AdministrativeRegionId).HasDatabaseName("idx_provinces_region");
        builder.HasIndex(p => p.AdministrativeUnitId).HasDatabaseName("idx_provinces_unit");
    }
}