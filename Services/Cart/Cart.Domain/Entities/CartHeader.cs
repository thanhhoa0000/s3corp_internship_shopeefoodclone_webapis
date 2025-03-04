namespace ShopeeFoodClone.WebApi.Cart.Domain.Entities;

public class CartHeader : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string? SessionId { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal TotalPrice { get; set; }
}
