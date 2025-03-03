namespace ShopeeFoodClone.WebApi.Users.Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateUserAsync(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        
        return result.Succeeded;
    }

    public async Task<IdentityResult> UpdateUserAsync(AppUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        
        return result;
    }

    public async Task<bool> AddUserToRoleAsync(AppUser user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);
        
        return result.Succeeded;
    }
}
