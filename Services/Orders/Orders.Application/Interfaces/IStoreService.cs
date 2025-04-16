namespace ShopeeFoodClone.WebApi.Orders.Application.Interfaces;

public interface IStoreService
{
    Task<Response?> GetStoreNameAsync(Guid storeId);
}
