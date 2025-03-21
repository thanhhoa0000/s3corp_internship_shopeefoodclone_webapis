namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public class VendorUpdateProductRequest : BaseProductRequest
{
    public Guid ConcurrencyStamp { get; set; }
}
