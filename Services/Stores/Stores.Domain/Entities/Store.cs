namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class Store : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid UserId { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required]
    public TimeOnly OpeningHour { get; set; }
    [Required]
    public TimeOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    
    public required StoreAddress StoreAddress { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}