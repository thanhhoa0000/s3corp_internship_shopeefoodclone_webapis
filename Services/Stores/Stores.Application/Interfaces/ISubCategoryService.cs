namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface ISubCategoryService
{
    Task<Response> GetAllByCategoryIdAsync(GetSubCategoriesRequest request);
    Task<Response> GetAllByCategoryCodeNameAsync(GetSubCategoriesRequest request);
    Task<Response> GetAsync(Guid subCateId);
    Task<Response> CreateAsync(CreateSubCategoryRequest request);
    Task<Response> UpdateAsync(UpdateSubCategoryRequest request);
    Task<Response> RemoveAsync(Guid subCateId);
    Task<Response> DeleteAsync(Guid subCateId);
}
