namespace ShopeeFoodClone.WebApi.Identity.Application.Dtos;

public class RegistrationRequest
{
    [Required, MinLength(6)]
    public required string UserName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
    [Phone, MaxLength(12)]
    public string? PhoneNumber { get; set; }
    [MinLength(2), MaxLength(30)]
    public string? FirstName { get; set; }
    [MinLength(2), MaxLength(50)]
    public string? LastName { get; set; }
    public Role Role { get; set; }
}
