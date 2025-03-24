namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public sealed class GetProductsRequest : BaseSearchRequest
{
    public Guid StoreId { get; set; }
}
