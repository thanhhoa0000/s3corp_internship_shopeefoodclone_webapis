namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface ISubCategoryService
{
    Task<Response> GetAllByCategoryIdAsync(GetSubCategoriesRequest request);
    Task<Response> GetAllByCategoryCodeNameAsync(GetSubCategoriesRequest request);
    Task<Response> GetAsync(Guid subCateId);
    Task<Response> CreateAsync(CreateSubCategoryRequest request);
    Task<Response> UpdateAsync(SubCategoryDto subCategoryDto);
    Task<Response> RemoveAsync(Guid subCateId);
}
