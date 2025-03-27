namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public class BaseMenuRequest
{
    public Guid Id { get; set; }
    [Required]
    public Guid StoreId { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }
    public MenuState State { get; set; } = MenuState.Active;
    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
}