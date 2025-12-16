using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllersж
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWishlistService _wishlistService;

        public WishlistController(UserManager<AppUser> userManager,IWishlistService wishlistService)
        {
            _userManager = userManager;
            _wishlistService = wishlistService;
        }
       
        
        [HttpPost]
        public async Task<IActionResult> Toggle(int productId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Json(new { success = false, message = "unauthorized" });

            bool isAdded = await _wishlistService.ToggleWishlistAsync(productId, userId);

            int count = await _wishlistService.GetWishlistCountAsync(userId);

            return Json(new { success = true, isAdded,count });
        }

        public async Task<IActionResult> MiniHeader()
        {
            string? userId = _userManager.GetUserId(User);

            int count = 0;

            if (userId != null)
            {
                count = await _wishlistService.GetWishlistCountAsync(userId);
            }

            return PartialView("_WishlistHeader", count);
        }
        
        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
                return Json(new { success = false, message = "unauthorized" });

            bool removed = await _wishlistService.RemoveFromWishlistAsync(productId, userId);

            int count = await _wishlistService.GetWishlistCountAsync(userId);

            return Json(new { success = removed, count });
        }

        public async Task<IActionResult> LoadList()
        {
            
            await Task.CompletedTask;
            return ViewComponent("Wishlist");
        }

        public async Task<IActionResult> Index()
        {
            var items = await _wishlistService.GetUsersDetailWishlistAsync(_userManager.GetUserId(User));

            return View(items);
        }


       


    }
}
