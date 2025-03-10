namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(x => x.Code);
        
        builder.HasOne(p => p.Province)
            .WithMany()
            .HasForeignKey(p => p.ProvinceCode)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("districts_province_code_fkey");
        
        builder.HasOne(p => p.AdministrativeUnit)
            .WithMany()
            .HasForeignKey(p => p.AdministrativeUnitId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("districts_administrative_unit_id_fkey");

        builder.HasIndex(p => p.ProvinceCode).HasDatabaseName("idx_districts_province");
        builder.HasIndex(p => p.AdministrativeUnitId).HasDatabaseName("idx_districts_unit");   
    }
}