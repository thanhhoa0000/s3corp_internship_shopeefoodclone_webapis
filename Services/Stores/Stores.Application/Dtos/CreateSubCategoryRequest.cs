namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record CreateSubCategoryRequest(SubCategoryDto SubCategoryDto, Guid CategoryId);