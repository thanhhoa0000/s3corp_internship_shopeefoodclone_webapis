namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class AdministrativeRegion
{
    [Key]
    public byte Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string NameEng { get; set; }
    public string? CodeName { get; set; }
    public string? CodeNameEng { get; set; }
}