namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface ICollectionService
{
    Task<Response> GetByLocationAsync(GetCollectionsByLocationRequest request, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetByLocationAndCategoryAsync(GetCollectionsRequest request, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid collectionId);
    Task<Response> CreateAsync(CreateCollectionRequest request);
    Task<Response> RemoveAsync(Guid collectionId);
    Task<Response> UpdateAsync(CollectionDto collectionDto);
}
