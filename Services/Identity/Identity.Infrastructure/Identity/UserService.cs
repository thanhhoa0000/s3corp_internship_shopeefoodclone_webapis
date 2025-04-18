﻿namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> CreateUserAsync(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        
        return result.Succeeded;
    }

    public async Task<bool> AddUserToRoleAsync(AppUser user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);
        
        return result.Succeeded;
    }

    public async Task<IList<string>> GetUserRolesAsync(AppUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}