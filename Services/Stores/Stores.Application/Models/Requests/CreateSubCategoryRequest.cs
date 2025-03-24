namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Requests;

public sealed class CreateSubCategoryRequest
{
    public Guid CategoryId { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public required string Name { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public required string CodeName { get; set; }
}
