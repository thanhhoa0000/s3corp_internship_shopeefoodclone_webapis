namespace ShopeeFoodClone.WebApi.Orders.Application.Dtos;

public sealed record GetOrdersRequest(Guid CustomerId, OrderStatus Status, SortingOrder SortingOrder = SortingOrder.Descending);
