namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface ICollectionService
{
    Task<Response> GetByLocationAndCategoryAsync(GetCollectionsRequest request);
    Task<Response> GetAsync(Guid collectionId);
    Task<Response> CreateAsync(CreateCollectionRequest request);
    Task<Response> RemoveAsync(Guid collectionId);
    Task<Response> UpdateAsync(CollectionDto collectionDto);
}
