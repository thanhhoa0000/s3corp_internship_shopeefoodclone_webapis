namespace ShopeeFoodClone.WebApi.Identity.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    [MinLength(2), MaxLength(30)]
    public string? FirstName { get; set; }
    [MinLength(2), MaxLength(50)]
    public string? LastName { get; set; }
}