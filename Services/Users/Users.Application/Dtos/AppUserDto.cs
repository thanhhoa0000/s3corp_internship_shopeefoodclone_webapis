namespace ShopeeFoodClone.WebApi.Users.Application.Dtos;

public class AppUserDto : IEntity
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [Phone]
    public required string PhoneNumber { get; set; }
    [MinLength(2), MaxLength(30)]
    public string? FirstName { get; set; }
    [MinLength(2), MaxLength(50)]
    public string? LastName { get; set; }

}