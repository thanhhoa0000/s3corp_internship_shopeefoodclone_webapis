namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class Category : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    public Guid ConcurrencyStamp  { get; set; }
    
    public ICollection<Store> Stores { get; set; } = new List<Store>();
}