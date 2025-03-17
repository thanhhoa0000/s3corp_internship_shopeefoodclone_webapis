namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record GetCollectionsRequest(GetCollectionsByLocationRequest LocationRequest, string CategoryName);
