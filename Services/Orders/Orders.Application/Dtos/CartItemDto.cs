namespace ShopeeFoodClone.WebApi.Orders.Application.Dtos;

public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid CartHeaderId { get; set; }
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    public ProductDto? Product { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
}
