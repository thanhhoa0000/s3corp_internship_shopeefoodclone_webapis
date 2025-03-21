namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Requests;

public sealed record CreateCollectionRequest(CollectionDto? Collection, List<Guid> StoreIds);
