namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public sealed record GetStoreRequest(GetStoreByLocationRequest LocationRequest, string CategoryName);
