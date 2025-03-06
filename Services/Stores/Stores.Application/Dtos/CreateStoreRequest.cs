namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public record CreateStoreRequest(StoreDto Store, ICollection<CategoryDto> Categories, StoreAddressDto Address);
