namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IDistrictService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
}