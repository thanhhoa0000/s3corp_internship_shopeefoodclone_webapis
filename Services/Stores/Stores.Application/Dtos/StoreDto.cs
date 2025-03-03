namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public class StoreDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public DateOnly OpeningHour { get; set; }
    public DateOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    
    public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}