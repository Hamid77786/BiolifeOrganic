using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class WishlistViewComponent:ViewComponent
{
    private readonly IWishlistService _wishlistService;
    private readonly UserManager<AppUser> _userManager;

    public WishlistViewComponent(IWishlistService wishlistService, UserManager<AppUser> userManager)
    {
        _wishlistService = wishlistService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        var items = await _wishlistService.GetUsersDetailWishlistAsync(userId);
        
        return View(items);
    }
}
