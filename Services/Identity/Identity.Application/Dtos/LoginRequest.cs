namespace ShopeeFoodClone.WebApi.Identity.Application.Dtos;

public sealed class LoginRequest
{
    [Required, MinLength(6)]
    public required string UserName { get; set; }
    [Required, DataType(DataType.Password), MinLength(8)]
    public required string Password { get; set; }
}