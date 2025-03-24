namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Requests;

public class GetStoresByVendorIdRequest : BaseSearchRequest
{
    public Guid VendorId { get; set; }
}
