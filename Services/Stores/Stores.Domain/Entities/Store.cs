namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class Store : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required]
    public required string Address { get; set; }
    [Required]
    public DateOnly OpeningHour { get; set; }
    [Required]
    public DateOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}