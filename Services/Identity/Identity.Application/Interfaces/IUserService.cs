using Microsoft.AspNetCore.Identity.Data;

namespace ShopeeFoodClone.WebApi.Identity.Application.Interfaces;

public interface IUserService
{
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    Task<bool> CreateUserAsync(AppUser user, string password);
    Task<bool> AddUserToRoleAsync(AppUser user, string roleName);
    Task<IList<string>> GetUserRolesAsync(AppUser user);
}
