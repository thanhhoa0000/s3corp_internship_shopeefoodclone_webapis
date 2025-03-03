namespace ShopeeFoodClone.WebApi.Users.Application.Interfaces;

public interface IRoleManagementService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid roleId);
    Task<Response> CreateAsync(string roleName);
    Task<Response> RemoveAsync(Guid roleId);
    Task<Response> UpdateAsync(AppRoleDto roleDto);
}