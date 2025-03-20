namespace ShopeeFoodClone.WebApi.Stores.Application.Requests;

public sealed record CreateSubCategoryRequest(SubCategoryDto SubCategoryDto, Guid CategoryId);