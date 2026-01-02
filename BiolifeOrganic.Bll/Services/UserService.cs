using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Bll.ViewModels.User;
using BiolifeOrganic.Bll.ViewModels.UserDiscount;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services;

public class UserService:IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager,IAppUserRepository appUserRepository, IMapper mapper)
    {
        _userManager = userManager;
        _userRepository = appUserRepository;
        _mapper = mapper;
    }

    public async Task<bool> IsBlockedAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.LockoutEnd > DateTimeOffset.UtcNow;
    }

    public async Task<bool> IsAdminAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null && await _userManager.IsInRoleAsync(user, "Admin");
    }

   

    public async Task ToggleBlockAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;

        if (await _userManager.IsInRoleAsync(user, "Admin"))
            return;

        if (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow)
            user.LockoutEnd = null;
        else
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);

        await _userManager.UpdateAsync(user);
    }

    public async Task ToggleAdminRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;

        const string adminRole = "Admin";

        if (await _userManager.IsInRoleAsync(user, adminRole))
        {
            await _userManager.RemoveFromRoleAsync(user, adminRole);
        }
        else
        {
           await _userManager.AddToRoleAsync(user, adminRole);
        }
    }



    public async Task<List<UserViewModel>> GetAllUsersWithDetailsAsync()
    {
        var users = await _userRepository.GetAllUsersForAdminAsync();
        return _mapper.Map<List<UserViewModel>>(users);
    }

    public async Task<UserDetailsViewModel?> GetUserDetailsAsync(string userId)
    {
        var user = await _userRepository.GetUserDetailsAsync(userId);
        return user == null ? null : _mapper.Map<UserDetailsViewModel>(user);
    }

    public async Task<DeleteUserResult> DeleteUserAsync(string targetUserId, string currentUserId)
    {
        var user = await _userManager.FindByIdAsync(targetUserId);
        if (user == null)
            return new DeleteUserResult { Success = false, ErrorMessage = "User not found" };

        if (await _userManager.IsInRoleAsync(user, "Admin"))
            return new DeleteUserResult { Success = false, ErrorMessage = "Admin users cannot be deleted" };

        if (user.Id == currentUserId)
            return new DeleteUserResult { Success = false, ErrorMessage = "You cannot delete yourself" };

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return new DeleteUserResult
            {
                Success = false,
                ErrorMessage = string.Join(", ", updateResult.Errors.Select(e => e.Description))
            };
        }

        return new DeleteUserResult { Success = true };
    }

    public async Task<List<UserViewModel>> GetDeletedUsersAsync()
    {
        var users = await _userRepository.GetDeletedUsersAsync();
        return _mapper.Map<List<UserViewModel>>(users);
    }

    public async Task<bool> RestoreUserAsync(string userId)
    {
        var user = await _userManager.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null || !user.IsDeleted)
            return false;

        user.IsDeleted = false;
        user.DeletedAt = null;
        user.UpdatedAt = DateTime.UtcNow;

        user.LockoutEnd = null;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }





}








