namespace ShopeeFoodClone.WebApi.Cart.Application.Interfaces;

public interface ICartService
{
    Task<Response> GetCartAsync(Guid customerId);
    Task<Response> AddToCartAsync(AddToCartRequest request);
    Task<Response> RemoveFromCartAsync(Guid cartItemId);
}
