namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Identity;

public class JwtProperties
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Key { get; set; }
    public int? Expiration { get; set; }
}