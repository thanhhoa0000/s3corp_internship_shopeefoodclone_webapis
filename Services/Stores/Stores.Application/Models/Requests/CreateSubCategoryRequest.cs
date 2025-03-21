namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Requests;

public sealed record CreateSubCategoryRequest(SubCategoryDto SubCategoryDto, Guid CategoryId);