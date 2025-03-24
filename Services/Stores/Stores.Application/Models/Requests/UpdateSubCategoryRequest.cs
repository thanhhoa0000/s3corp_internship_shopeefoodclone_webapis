namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Requests;

public sealed class UpdateSubCategoryRequest
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(2), MaxLength(50)]
    public required string CodeName { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}
