namespace ShopeeFoodClone.WebApi.Stores.Application.Requests;

public sealed class GetCollectionsRequest : BaseSearchRequest
{
    [Required]
    public LocationRequest? LocationRequest { get; set; }
    public string? CategoryName { get; set; }
}
