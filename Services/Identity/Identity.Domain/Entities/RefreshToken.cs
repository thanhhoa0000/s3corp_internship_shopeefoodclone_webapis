namespace ShopeeFoodClone.WebApi.Identity.Domain.Entities;

public class RefreshToken
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid AppUserId { get; set; }
    [Required]
    public required string Token { get; set; }
    public DateTime ExpireTime { get; set; }
    public AppUser? AppUser { get; set; }
}
