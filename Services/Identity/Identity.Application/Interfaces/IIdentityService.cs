namespace ShopeeFoodClone.WebApi.Identity.Application.Interfaces;

public interface IIdentityService
{
    Task<Response> LoginAsync(LoginRequest request, int refreshTokenExpirationTimeInDays); 
    Task<Response> LoginWithRefreshTokenAsync(LoginRefreshTokenRequest request, int refreshTokenExpirationTimeInDays);
    Task<Response> RegisterAsync(RegistrationRequest request);
}