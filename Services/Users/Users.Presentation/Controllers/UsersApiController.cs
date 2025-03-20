namespace ShopeeFoodClone.WebApi.Users.Presentation.Controllers;

[AllowAnonymous]
[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(1)]
public class UsersApiController : ControllerBase
{
    private readonly IUserManagementService _service;
    private readonly ILogger<UsersApiController> _logger;
    private Response _response;

    public UsersApiController(IUserManagementService service, ILogger<UsersApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }

    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        try
        {
            var request = new GetUsersRequest()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            
            _logger.LogInformation("Getting the users...");
            
            _response = await _service.GetAllAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the users!");
        }
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        try
        {
            _logger.LogInformation("Getting the user...");
            
            _response = await _service.GetAsync(userId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the user!");
        }
    }

    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            _logger.LogInformation($"Creating user {request.User.Id}");
            
            _response = await _service.CreateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the user!");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AppUserDto userDto)
    {
        try
        {
            _logger.LogInformation($"Updating user {userDto.Id}");
            
            _response = await _service.UpdateAsync(userDto);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the user!");
        }
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        try
        {
            _logger.LogInformation($"Deleting user {userId}");
            
            _response = await _service.RemoveAsync(userId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the user!");
        }
    }
}
