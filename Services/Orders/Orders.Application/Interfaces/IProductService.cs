namespace ShopeeFoodClone.WebApi.Orders.Application.Interfaces;

public interface IProductService
{
    Task<Response?> GetProductAsync(Guid productId);
}
