namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        builder.HasKey(x => x.Code);
        
        builder.HasOne(p => p.District)
            .WithMany()
            .HasForeignKey(p => p.DistrictCode)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("wards_district_code_fkey");
        
        builder.HasOne(p => p.AdministrativeUnit)
            .WithMany()
            .HasForeignKey(p => p.AdministrativeUnitId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("wards_administrative_unit_id_fkey");

        builder.HasIndex(p => p.DistrictCode).HasDatabaseName("idx_wards_district");
        builder.HasIndex(p => p.AdministrativeUnitId).HasDatabaseName("idx_wards_unit");
    }
}