namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public sealed class GetMenusRequest : BaseSearchRequest
{
    public Guid StoreId { get; set; }    
}
