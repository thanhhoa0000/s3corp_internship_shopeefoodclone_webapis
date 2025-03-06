namespace ShopeeFoodClone.WebApi.Cart.Application.Interfaces;

public interface IProductService
{
    Task<Response?> GetProductAsync(Guid productId);
}