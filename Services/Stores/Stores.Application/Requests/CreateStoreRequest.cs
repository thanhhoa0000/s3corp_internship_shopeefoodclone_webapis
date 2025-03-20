namespace ShopeeFoodClone.WebApi.Stores.Application.Requests;

public sealed record CreateStoreRequest(StoreDto Store, List<Guid> SubCateIds, string WardCode);
