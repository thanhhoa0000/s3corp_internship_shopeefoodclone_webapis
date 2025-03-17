namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record GetCollectionsByLocationRequest(string? Province, string? District, string? Ward);
