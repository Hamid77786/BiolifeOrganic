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

    public async Task<List<UserViewModel>> GetAllUsersWithDetailsAsync()
    {
        var users = await _userRepository.GetAllUsersWithDetailsAsync();

        var usersVm = _mapper.Map<List<UserViewModel>>(users);

        foreach (var userVm in usersVm)
        {
            var user = users.First(u => u.Id == userVm.Id); 
            userVm.IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        }

        return usersVm; 
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

    public async Task<UserDetailsViewModel?> GetUserDetailsAsync(string userId)
    {
        var user = await _userRepository.GetUserWithDetailsAsync(userId);
        if (user == null) return null;

        var model = _mapper.Map<UserDetailsViewModel>(user);

        model.Orders = _mapper.Map<List<OrderDetailsViewModel>>(user.Orders);

        var wishlistItems = user.Wishlists
        .Select(w => _mapper.Map<WishlistItemViewModel>(w))
        .ToList();

        model.Wishlists = new List<WishlistViewModel>
        {
            new WishlistViewModel
            {
                AppUserId = user.Id,
                AppUserName = user.FullName ?? user.UserName,
                Count = wishlistItems.Count,
                Items = wishlistItems
            }
        };


        model.Discounts = _mapper.Map<List<UserDiscountViewModel>>(user.UserDiscounts);

        model.IsBlocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow;

        return model;

    }

}








