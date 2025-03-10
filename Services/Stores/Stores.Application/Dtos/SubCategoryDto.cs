namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public class SubCategoryDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }

    [Required, MinLength(4), MaxLength(50)]
    public required string Name { get; set; }

    public Guid ConcurrencyStamp { get; set; }
    
    public Category? Category { get; set; }
}