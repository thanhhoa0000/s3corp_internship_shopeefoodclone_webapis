namespace ShopeeFoodClone.WebApi.Cart.Application.Events;

public class ProductInfoRequest
{
    public IEnumerable<Guid>? ProductIds { get; set; }
}
