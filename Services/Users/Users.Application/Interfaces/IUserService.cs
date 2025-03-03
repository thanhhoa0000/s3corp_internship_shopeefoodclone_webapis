namespace ShopeeFoodClone.WebApi.Users.Application.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(AppUser user, string password);
    
    Task<IdentityResult> UpdateUserAsync(AppUser user);
    Task<bool> AddUserToRoleAsync(AppUser user, string roleName);
} 
