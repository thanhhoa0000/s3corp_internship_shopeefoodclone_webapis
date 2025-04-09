namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface ICategoryService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid cateId);
    Task<Response> GetByCodeNameAsync(string name);
    Task<Response> CreateAsync(CategoryDto categoryDto);
    Task<Response> RemoveAsync(Guid cateId);
    Task<Response> DeleteAsync(Guid cateId);
    Task<Response> UpdateAsync(CategoryDto categoryDto);
}
