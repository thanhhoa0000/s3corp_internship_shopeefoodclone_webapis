namespace ShopeeFoodClone.WebApi.Products.Application.Events;

public class ProductInfoRequest
{
    public IEnumerable<Guid>? ProductIds { get; set; }
}
