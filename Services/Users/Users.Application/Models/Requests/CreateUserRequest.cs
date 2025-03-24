namespace ShopeeFoodClone.WebApi.Users.Application.Models.Requests;

public sealed class CreateUserRequest
{
    public required AppUserDto User { get; set; }
    [DataType(DataType.Password)]
    public required string DefaultPassword { get; set; } = "Aa@123456";
    public required Role Role { get; set; } = Role.Admin;
}