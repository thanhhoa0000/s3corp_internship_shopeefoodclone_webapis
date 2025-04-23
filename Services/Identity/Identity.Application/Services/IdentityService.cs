using LoginRequest = ShopeeFoodClone.WebApi.Identity.Application.Models.Dtos.LoginRequest;

namespace ShopeeFoodClone.WebApi.Identity.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly IIdentityRepository _repository;
    private readonly IUserService _service;
    private readonly ITokenProvider _tokenProvider;
    private readonly IMapper _mapper;

    public IdentityService(
        IIdentityRepository repository, 
        IMapper mapper, 
        IUserService service,
        ITokenProvider tokenProvider)
    {
        _repository = repository;
        _mapper = mapper;
        _service = service;
        _tokenProvider = tokenProvider;
    }

    /// <summary>
    /// Authenticates a user using their username and password.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <param name="refreshTokenExpirationTimeInDays">The expiration time for the refresh token in days.</param>
    /// <returns>Returns access token and refresh token.</returns>
    public async Task<Response> LoginAsync(LoginRequest request, int refreshTokenExpirationTimeInDays)
    {
        var response = new Response();
        
        try
        {
            var user = await _repository.GetUserAsync(u => u.UserName == request.UserName);

            bool isUserValid = await _service.CheckPasswordAsync(user, request.Password);

            if (!isUserValid)
            {
                response.IsSuccessful = false;
                response.Message = "Username or password is incorrect!";

                return response;
            }

            IEnumerable<Role> roles = (await _service.GetUserRolesAsync(user))
                .Select(r => (Role)Enum.Parse(typeof(Role), r));

            var token = _tokenProvider.CreateAccessToken(user, roles);

            var refreshTokenInDb = await _repository.GetRefreshTokenAsync(r => r.AppUserId == user.Id);

            if (refreshTokenInDb is not null)
            {
                refreshTokenInDb.Token = _tokenProvider.GenerateRefreshToken();
                refreshTokenInDb.ExpireTime = DateTime.UtcNow.AddDays(refreshTokenExpirationTimeInDays);
                
                await _repository.UpdateRefreshTokenAsync(refreshTokenInDb);
                
                response.Body = new LoginResponse()
                {
                    AccessToken = token,
                    RefreshToken = refreshTokenInDb.Token,
                };

                return response;
            }

            var refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                AppUserId = user.Id,
                Token = _tokenProvider.GenerateRefreshToken(),
                ExpireTime = DateTime.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
            };

            await _repository.CreateRefreshTokenAsync(refreshToken);

            response.Body = new LoginResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token,
            };

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }

    /// <summary>
    /// Authenticates a user using refresh token.
    /// </summary>
    /// <param name="request">The login request containing refresh token.</param>
    /// <param name="refreshTokenExpirationTimeInDays">The expiration time for the refresh token in days.</param>
    /// <returns>Returns access token and refresh token.</returns>
    public async Task<Response> LoginWithRefreshTokenAsync(LoginRefreshTokenRequest request, int refreshTokenExpirationTimeInDays)
    {
        var response = new Response();

        try
        {
            Console.WriteLine(request.RefreshToken);
            var refreshToken = await _repository.GetRefreshTokenAsync(
                include: q => q.Include(r => r.AppUser),
                filter: r => r.Token == request.RefreshToken,
                tracked: false);

            if (refreshToken is null)
            {
                response.IsSuccessful = false;
                response.Message = "Refresh token not found!";

                return response;
            }
            
            if (refreshToken.ExpireTime < DateTime.UtcNow)
            {
                response.IsSuccessful = false;
                response.Message = "Refresh token is expired!";

                return response;
            }
            
            IEnumerable<Role> roles = (await _service.GetUserRolesAsync(refreshToken.AppUser!))
                .Select(r => (Role)Enum.Parse(typeof(Role), r));
                
            string accessToken = _tokenProvider.CreateAccessToken(refreshToken.AppUser!, roles);
                
            refreshToken.Token = _tokenProvider.GenerateRefreshToken();
            
            refreshToken.ExpireTime = DateTime.UtcNow.AddDays(refreshTokenExpirationTimeInDays);
                
            await _repository.UpdateRefreshTokenAsync(refreshToken);

            response.Body = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };

            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.ToString();
            
            return response;
        }
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="request">The registration request containing user details.</param>
    /// <returns>Returns a response indicating the success or failure of the registration.</returns>
    public async Task<Response> RegisterAsync(RegistrationRequest request)
    {
        var response = new Response();

        try
        {
            var user = _mapper.Map<AppUser>(request);
            
            var result = await _service.CreateUserAsync(user, request.Password);
            
            if (result)
                await _service.AddUserToRoleAsync(user, request.Role.ToString());
            
            response.Message = "Register successfully!";
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }
}
