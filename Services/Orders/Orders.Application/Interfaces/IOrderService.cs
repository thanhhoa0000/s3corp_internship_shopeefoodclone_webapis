namespace ShopeeFoodClone.WebApi.Orders.Application.Interfaces;

public interface IOrderService
{
    Task<Response> GetOrdersByCustomerIdAsync(GetOrdersRequest request);
    Task<Response> GetOrderByIdAsync(Guid orderId);
    Task<Response> CreateOrderAsync(CreateOrderRequest request);
    Task<Response> SoftDeleteOrderAsync(Guid orderId);
}
