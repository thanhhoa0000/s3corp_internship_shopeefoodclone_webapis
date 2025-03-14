namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class Store : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid UserId { get; set; }
    [MaxLength(20)]
    public string? WardCode { get; set; }
    [MaxLength(100)]
    public string? StreetName { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required]
    public TimeOnly OpeningHour { get; set; }
    [Required]
    public TimeOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    public int Sold { get; set; } = 0;
    public bool IsPromoted { get; set; } = false;
    
    public Ward? Ward { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}