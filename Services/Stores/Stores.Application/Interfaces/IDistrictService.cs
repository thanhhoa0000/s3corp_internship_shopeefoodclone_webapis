namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IDistrictService
{
    Task<Response> GetAllByProvinceAsync(string province, int pageSize = 0, int pageNumber = 1);
}
