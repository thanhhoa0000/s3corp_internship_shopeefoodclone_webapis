namespace ShopeeFoodClone.WebApi.Users.Application.Services;

public class RoleManagementService : IRoleManagementService
{
    private readonly IRoleRepository _repository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public RoleManagementService(IRoleRepository repository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _repository = repository;
        _httpContext = httpContextAccessor.HttpContext!;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of roles
    /// </summary>
    /// <param name="pageSize">Pages number to get roles</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>List of roles</returns>
    public async Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();
        
        try
        {
            var roles = await _repository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);

            var pagination = new Pagination()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };
            
            _httpContext.Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagination);
            
            response.Body = _mapper.Map<IEnumerable<AppRoleDto>>(roles);
            
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
    /// Get a single role
    /// </summary>
    /// <param name="roleId">The ID of the role</param>
    /// <returns>The role</returns>
    public async Task<Response> GetAsync(Guid roleId)
    {
        var response = new Response();

        try
        {
            var role = await _repository.GetAsync(u => u.Id == roleId, tracked: false);

            response.Body = _mapper.Map<AppRoleDto>(role);
            
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
    /// Create an Admin role
    /// </summary>
    /// <param name="roleName">The role name to create</param>
    /// <returns>The created role</returns>
    public async Task<Response> CreateAsync(string roleName)
    {
        var response = new Response();

        try
        {
            var role = new AppRole()
            {
                Id = Guid.NewGuid(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            await _repository.CreateAsync(role);
            
            response.Body = _mapper.Map<AppRoleDto>(role);
            
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
    /// Update the role's metadata
    /// </summary>
    /// <param name="roleDto">The role to update</param>
    /// <returns>The updated role</returns>
    public async Task<Response> UpdateAsync(AppRoleDto roleDto)
    {
        var response = new Response();

        try
        {
            var role = await _repository.GetAsync(u => u.Id == roleDto.Id, tracked: false);

            if (role.ConcurrencyStamp != roleDto.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            var roleToUpdate = _mapper.Map<AppRole>(roleDto);
            await _repository.UpdateAsync(roleToUpdate);
            
            response.Body = _mapper.Map<AppRoleDto>(roleToUpdate);

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
    /// Detete the role with the given role's ID
    /// </summary>
    /// <param name="roleId">The role's ID to delete</param>
    /// <returns>The deleted role</returns>
    public async Task<Response> RemoveAsync(Guid roleId)
    {
        var response = new Response();

        try
        {
            var role = await _repository.GetAsync(u => u.Id == roleId, tracked: false);
            await _repository.RemoveAsync(role);

            response.Body = _mapper.Map<AppRoleDto>(role);

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