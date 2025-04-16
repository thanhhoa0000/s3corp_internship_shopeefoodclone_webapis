namespace ShopeeFoodClone.WebApi.Orders.Application.Interfaces;

public interface ICartService
{
    Task<bool> EmptyCart(Guid customerId);
}
