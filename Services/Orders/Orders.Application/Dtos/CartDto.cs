namespace ShopeeFoodClone.WebApi.Orders.Application.Dtos;

public class CartDto
{
    public CartHeaderDto? CartHeader { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
}
