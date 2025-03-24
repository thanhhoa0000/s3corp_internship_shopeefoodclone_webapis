namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Requests;

public class AdminUpdateStoreRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsPromoted { get; set; } = false;
    public StoreState State { get; set; } = StoreState.Active;
    public Guid ConcurrencyStamp { get; set; }
}
