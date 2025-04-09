namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IWardService
{
    Task<Response> GetNamesAsync(string district, int pageSize= 0, int pageNumber = 1);    
}
