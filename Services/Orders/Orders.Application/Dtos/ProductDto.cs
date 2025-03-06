namespace ShopeeFoodClone.WebApi.Orders.Application.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(20), MaxLength(100)]
    public required string Description { get; set; }

    [Required] public int AvailableStock { get; set; } = 0;

    public int BookingCount { get; set; } = 0;
    public string? CoverImagePath { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}
