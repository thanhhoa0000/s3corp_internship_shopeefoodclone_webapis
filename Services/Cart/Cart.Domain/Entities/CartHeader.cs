namespace ShopeeFoodClone.WebApi.Cart.Domain.Entities;

public class CartHeader : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal TotalPrice { get; set; } = 0;
}
