namespace ShopeeFoodClone.WebApi.Cart.Application.Dtos;

public class CartDto
{
    public required CartHeaderDto CartHeader { get; set; }
    public ICollection<CartItemDto>? CartItems { get; set; }
}
