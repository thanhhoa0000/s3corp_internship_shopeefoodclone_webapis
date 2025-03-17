namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record CreateCollectionRequest(CollectionDto Collection, List<Guid> StoreIds);
