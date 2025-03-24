namespace ShopeeFoodClone.WebApi.Identity.Application.Models.Dtos;

public class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}