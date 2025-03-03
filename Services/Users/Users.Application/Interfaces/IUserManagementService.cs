namespace ShopeeFoodClone.WebApi.Users.Application.Interfaces;

public interface IUserManagementService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid userId);
    Task<Response> CreateAsync(CreateUserRequest request);
    Task<Response> RemoveAsync(Guid userId);
    Task<Response> UpdateAsync(AppUserDto userDto);
}