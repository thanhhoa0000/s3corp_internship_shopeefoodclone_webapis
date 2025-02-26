namespace ShopeeFoodClone.WebApi.Identity.Application.Interfaces;

public interface ITokenProvider
{
    string CreateAccessToken(AppUser user, IEnumerable<Role> roles);
    string GenerateRefreshToken();
}