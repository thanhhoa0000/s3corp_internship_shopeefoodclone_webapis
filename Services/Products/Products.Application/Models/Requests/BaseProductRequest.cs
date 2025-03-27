namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public class BaseProductRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StoreId { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(20), MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required] public int AvailableStock { get; set; } = 0;
    public string? CoverImagePath { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
