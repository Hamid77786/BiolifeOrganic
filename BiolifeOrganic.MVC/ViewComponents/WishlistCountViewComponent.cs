using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class WishlistCountViewComponent:ViewComponent
{
    private readonly IWishlistService _wishlistService;
    private readonly UserManager<AppUser> _userManager;

    public WishlistCountViewComponent(
       IWishlistService wishlistService,
       UserManager<AppUser> userManager)
    {
        _wishlistService = wishlistService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        string? userId = _userManager.GetUserId(HttpContext.User);

        int count = userId != null ?
            await _wishlistService.GetWishlistCountAsync(userId) : 0;

       

        return View("Default",count);

    }
}
