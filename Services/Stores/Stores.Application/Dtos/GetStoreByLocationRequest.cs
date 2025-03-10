namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record GetStoreByLocationRequest(string? Province, string? District, string? Ward);
