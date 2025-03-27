namespace ShopeeFoodClone.WebApi.Products.Domain.Entities;

public class Product : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StoreId { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(20), MaxLength(200)]
    public required string Description { get; set; }

    [Required] public int AvailableStock { get; set; } = 0;

    public int BookingCount { get; set; } = 0;
    public string? CoverImagePath { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public Guid ConcurrencyStamp { get; set; }
    public ProductState State { get; set; } = ProductState.Normal;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    public ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
