namespace ShopeeFoodClone.WebApi.Stores.Application.Requests;

public class GetStoresByVendorIdRequest : BaseSearchRequest
{
    public Guid VendorId { get; set; }
}
