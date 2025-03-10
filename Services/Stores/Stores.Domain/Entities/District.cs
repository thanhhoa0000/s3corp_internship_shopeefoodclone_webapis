namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class District
{
    [Key]
    [MaxLength(20)]
    public required string Code { get; set; }
    [MaxLength(20)]
    public string? ProvinceCode { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? NameEng { get; set; }
    [Required]
    public required string FullName { get; set; }
    public string? FullNameEng { get; set; }
    public string? CodeName { get; set; }
    public byte? AdministrativeUnitId { get; set; }
    
    public Province? Province { get; set; }
    public AdministrativeUnit? AdministrativeUnit { get; set; }
}