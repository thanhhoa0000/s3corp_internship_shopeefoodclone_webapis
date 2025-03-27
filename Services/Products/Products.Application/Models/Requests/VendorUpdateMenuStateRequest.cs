namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public class VendorUpdateMenuStateRequest
{
    [Required]
    public Guid MenuId { get; set; }
    [Required]
    public MenuState State { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}
