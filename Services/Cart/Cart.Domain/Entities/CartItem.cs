namespace ShopeeFoodClone.WebApi.Cart.Domain.Entities;

public class CartItem : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CartHeaderId { get; set; }
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal Price { get; set; }
    public required CartHeader CartHeader { get; set; }
}
