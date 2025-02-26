namespace ShopeeFoodClone.WebApi.Identity.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    [StringLength(30)]
    public string? Description { get; set; }
}