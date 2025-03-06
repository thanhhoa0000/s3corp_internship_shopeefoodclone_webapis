namespace ShopeeFoodClone.WebApi.Orders.Domain.Enums;

public enum OrderStatus : byte
{
    Pending,
    Delivered,
    Canceled,
    DeletedByCustomer,
}
