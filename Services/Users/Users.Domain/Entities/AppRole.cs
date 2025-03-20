namespace ShopeeFoodClone.WebApi.Users.Domain.Entities;

public class AppRole : IdentityRole<Guid>, IEntity
{
    [StringLength(30)]
    public string? Description { get; set; }
}
