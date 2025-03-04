namespace ShopeeFoodClone.WebApi.Cart.Application.Dtos;

public class CartItemDto
{
    public int Index { get; set; }
    public Guid CartHeaderId { get; set; }
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
}
