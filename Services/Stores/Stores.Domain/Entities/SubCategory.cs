namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class SubCategory : IEntity
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(2), MaxLength(50)]
    public required string CodeName { get; set; }
    public Guid ConcurrencyStamp { get; set; }
    public CategoryState State { get; set; } = CategoryState.InUse;
    
    public Category? Category { get; set; }
    public ICollection<Store> Stores { get; set; } = new List<Store>();
}
