namespace ShopeeFoodClone.WebApi.Users.Application.Interfaces;

public interface IRoleManagementService
{
    Task<Response> GetAllAsync(GetRolesRequest request);
    Task<Response> GetAsync(Guid roleId);
    Task<Response> CreateAsync(string roleName);
    Task<Response> RemoveAsync(Guid roleId);
    Task<Response> UpdateAsync(AppRoleDto roleDto);
}
