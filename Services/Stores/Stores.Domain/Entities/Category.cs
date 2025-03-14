namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class Category : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, MinLength(2), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(2), MaxLength(50)]
    public required string CodeName { get; set; }
    public Guid ConcurrencyStamp  { get; set; }
}