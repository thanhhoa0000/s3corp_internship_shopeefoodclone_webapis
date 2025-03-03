namespace ShopeeFoodClone.WebApi.Users.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize = 0, [FromQuery] int pageNumber = 1)
        {
            try
            {
                _logger.LogInformation("Getting the users...");
                
                _response = await _service.GetAllAsync(pageSize:  pageSize, pageNumber: pageNumber);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when getting the users!");
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
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when getting the user!");
            }
        }

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
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when creating the user!");
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
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when updating the user!");
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
                _logger.LogError("Error(s) occured: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occured when deleting the user!");
            }
        }
    }
}
