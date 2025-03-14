namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record CreateStoreRequest(StoreDto Store, List<Guid> SubCateIds, string WardCode);
