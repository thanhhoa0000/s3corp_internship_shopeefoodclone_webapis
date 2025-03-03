namespace ShopeeFoodClone.WebApi.Identity.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion(1)]
    public class IdentityApiController : ControllerBase
    {
        private readonly IIdentityService _service;
        private readonly ILogger<IdentityApiController> _logger;
        private Response _response;
        private readonly int _refreshTokenExpiryInDays;

        public IdentityApiController(
            IIdentityService service,
            ILogger<IdentityApiController> logger,
            IConfiguration configuration)
        {
            _service = service;
            _logger = logger;
            _response = new Response();
            _refreshTokenExpiryInDays = configuration.GetValue<int>("RefreshTokenLifeTimeInDays");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation("Signing the user in...");

                _response = await _service.LoginAsync(request, _refreshTokenExpiryInDays);

                _logger.LogDebug(
                    $"Token expire time configuration: {_refreshTokenExpiryInDays}");
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when signing the user in!");
            }
        }

        [HttpPost("refresh_token_login")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] LoginRefreshTokenRequest request)
        {
            try
            {
                _logger.LogInformation("Signing the user in with the refresh token...");

                _response = await _service.LoginWithRefreshTokenAsync(request, _refreshTokenExpiryInDays);

                if (_response.Message.Contains("Refresh token is expired!"))
                {
                    _logger.LogError("Error(s) occured: \n---\n{error}", _response.Message);
                    
                    return BadRequest("Refresh token is expired!");
                }
                
                return Ok(_response);
                    
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when signing the user in with the refresh token!");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                _logger.LogInformation("Registering the user...");
                
                _response = await _service.RegisterAsync(request);

                if (!_response.IsSuccessful)
                {
                    _logger.LogError("Error(s) occured: \n---\n{error}", _response.Message);

                    return BadRequest("Error(s) occured when registering the user!");
                }
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);

                return BadRequest("Error(s) occured when registering the user!");
            }
        }
    }
}
