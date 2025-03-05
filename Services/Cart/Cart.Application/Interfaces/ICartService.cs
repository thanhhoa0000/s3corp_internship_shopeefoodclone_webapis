namespace ShopeeFoodClone.WebApi.Cart.Application.Interfaces;

public interface ICartService
{
    Task<Response> GetCartAsync(Guid customerId);
    Task<Response> AddToCartAsync(CartDto cartDto);
    Task<Response> RemoveFromCartAsync(Guid cartItemId);
    void LoadProductInfoForCart(IEnumerable<Guid> productIds);
}