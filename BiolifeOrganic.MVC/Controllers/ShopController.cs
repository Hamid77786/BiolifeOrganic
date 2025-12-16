using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Security.Claims;

namespace BiolifeOrganic.MVC.Controllers;


public class ShopController : Controller
{
    private readonly IShopService _shopService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IProductService _productService;
    private readonly IReviewService _reviewService;

    public ShopController(IReviewService reviewService,IShopService shopService,IProductService productService,UserManager<AppUser> userManager)
    {
        _shopService = shopService;
        _productService = productService;
        _userManager = userManager;
        _reviewService = reviewService;
    }
    public async Task<IActionResult> Index(int page=1,int pageSize=10, string? price ="all",
        string? availability ="all")
    {
        var shopViewModel = await _shopService.GetShopAsync(page,
            pageSize,
            price,
            availability);

        return View(shopViewModel);
    }
    public async Task<IActionResult> Detail(int id,int page =1)
    {
        var userId = _userManager.GetUserId(User);

        var productId = await _productService.GetByIdAsync(id);

        var shopViewModel = await _shopService.GetShopViewModel(id,userId,page,2);

        return View(shopViewModel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview([FromForm] ReviewViewModel model)
    {
        model.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        model.AppUserName = User.Identity!.Name;
        model.EmailAddress = User.FindFirstValue(ClaimTypes.Email) ?? model.AppUserName;

        if (model.AppUserId == null)
            return Unauthorized();

        if (model.ProductId == 0)
            return Json(new { success = false, message = "ProductId missing" });


        if (model.Stars < 1 || model.Stars > 5)
            return Json(new { success = false, message = "Invalid rating" });

        await _reviewService.AddReview(model);
        return Json(new { success = true });


    }

    public async Task<IActionResult> LoadComments(int productId, int page = 1)
    {
        var userId = _userManager.GetUserId(User);
        var model = await _shopService.GetShopViewModel(productId, userId, page, 2);

        return PartialView("_CommentsList", model);
    }

    public async Task<IActionResult> LoadRatingBlock(int productId)
    {
        var userId = _userManager.GetUserId(User);
        var model = await _shopService.GetShopViewModel(productId, userId, 1, 2);

        return PartialView("_RatingBlock", model);
    }

    public IActionResult LoadReviewCount(int productId)
    {
        var reviewCount = _reviewService.CountAsync(r => r.ProductId == productId).Result;

        return Content(reviewCount!.ToString() ?? "0");
    }




}
