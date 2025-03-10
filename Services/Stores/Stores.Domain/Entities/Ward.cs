namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class Ward
{
    [Key]
    [MaxLength(20)]
    public required string Code { get; set; }
    [MaxLength(20)]
    public string? DistrictCode { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? NameEng { get; set; }
    [Required]
    public required string FullName { get; set; }
    public string? FullNameEng { get; set; }
    public string? CodeName { get; set; }
    public byte? AdministrativeUnitId { get; set; }
    
    public District? District { get; set; }
    public AdministrativeUnit? AdministrativeUnit { get; set; }
}