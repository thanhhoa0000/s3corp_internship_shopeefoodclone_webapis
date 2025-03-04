namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public class StoreDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required]
    public required string Address { get; set; }
    [Required]
    public TimeOnly OpeningHour { get; set; }
    [Required]
    public TimeOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    
    public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}