using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Bll.ViewModels.User;
using BiolifeOrganic.Bll.ViewModels.UserDiscount;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Identity;

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


}








