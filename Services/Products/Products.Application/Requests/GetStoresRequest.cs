namespace ShopeeFoodClone.WebApi.Products.Application.Requests;

public sealed class GetStoresRequest : BaseSearchRequest
{
    public Guid StoreId { get; set; }
}
