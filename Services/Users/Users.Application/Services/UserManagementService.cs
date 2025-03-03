namespace ShopeeFoodClone.WebApi.Users.Application.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _repository;
    private readonly IUserService _service;
    HttpContext _httpContext;
    private readonly IMapper _mapper;

    public UserManagementService(
        IUserRepository repository,  
        IUserService service, 
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _httpContext = httpContextAccessor.HttpContext!;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of users
    /// </summary>
    /// <param name="pageSize">Pages number to get users</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>List of user</returns>
    public async Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();
        
        try
        {
            var users = await _repository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);

            var pagination = new Pagination()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };
            
            _httpContext.Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagination);
            
            response.Body = _mapper.Map<IEnumerable<AppUserDto>>(users);
            
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
    /// Get a single user
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <returns>The user</returns>
    public async Task<Response> GetAsync(Guid userId)
    {
        var response = new Response();

        try
        {
            var user = await _repository.GetAsync(u => u.Id == userId, tracked: false);

            response.Body = _mapper.Map<AppUserDto>(user);
            
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
    /// Create an Admin user
    /// </summary>
    /// <param name="request">The User to create and the needed information</param>
    /// <returns>The created user</returns>
    public async Task<Response> CreateAsync(CreateUserRequest request)
    {
        var response = new Response();

        try
        {
            var user = _mapper.Map<AppUser>(request.User);
            
            var result = await _service.CreateUserAsync(user, request.DefaultPassword);

            if (result)
                await _service.AddUserToRoleAsync(user, request.Role.ToString());
            
            response.Body = _mapper.Map<AppUserDto>(user);
            
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
    /// Update the User's metadata
    /// </summary>
    /// <param name="userDto">The User to update</param>
    /// <returns>The updated user</returns>
    public async Task<Response> UpdateAsync(AppUserDto userDto)
    {
        var response = new Response();

        try
        {
            var user = await _repository.GetAsync(u => u.Id == userDto.Id);

            var result = await _service.UpdateUserAsync(user);

            if (result.Errors.Any(e => e.Code == "ConcurrencyFailure"))
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            response.Body = _mapper.Map<AppUserDto>(user);

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
    /// Detete the user with the given user's ID
    /// </summary>
    /// <param name="userId">The User's ID of the user to delete</param>
    /// <returns>The deleted user</returns>
    public async Task<Response> RemoveAsync(Guid userId)
    {
        var response = new Response();

        try
        {
            var user = await _repository.GetAsync(u => u.Id == userId, tracked: false);
            await _repository.RemoveAsync(user);

            response.Body = _mapper.Map<AppUserDto>(user);

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