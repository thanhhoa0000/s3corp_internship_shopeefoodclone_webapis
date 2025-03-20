namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface ISubCategoryService
{
    Task<Response> GetAllAsync(Guid cateId, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAllByCodeNameAsync(string cateName, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid subCateId);
    Task<Response> CreateAsync(CreateSubCategoryRequest request);
    Task<Response> UpdateAsync(SubCategoryDto subCategoryDto);
    Task<Response> RemoveAsync(Guid subCateId);
}
