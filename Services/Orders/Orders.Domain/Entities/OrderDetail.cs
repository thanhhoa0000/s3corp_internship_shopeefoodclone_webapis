namespace ShopeeFoodClone.WebApi.Orders.Domain.Entities;

public class OrderDetail : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    [Required]
    public required string ProductName { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal Price { get; set; }
    public required Order Order { get; set; }
}
