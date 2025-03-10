namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class AdministrativeUnit
{
    [Key]
    public byte Id { get; set; }
    public string? FullName { get; set; }
    public string? FullNameEng { get; set; }
    public string? ShortName { get; set; }
    public string? ShortNameEng { get; set; }
    public string? CodeName { get; set; }
    public string? CodeNameEng { get; set; }
}