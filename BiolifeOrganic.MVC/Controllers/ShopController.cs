using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers;


public class ShopController : Controller
{
    private readonly IShopService _shopService;
    private readonly IProductService _productService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IReviewService _reviewService;

    public ShopController(IReviewService reviewService,IShopService shopService,IProductService productService,UserManager<AppUser> userManager)
    {
        _shopService = shopService;
        _productService = productService;
        _userManager = userManager;
        _reviewService = reviewService;
    }
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Detail(int id)
    {
        var userId = _userManager.GetUserId(User);

        var productId = await _productService.GetByIdAsync(id);

        var shopViewModel = await _shopService.GetShopViewModel(id,userId);

        return View(shopViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> AddReview(ReviewViewModel model)
    {
       

        if (model.Stars < 1 || model.Stars > 5)
            return Json(new { success = false, message = "Invalid rating" });

        await _reviewService.AddReview(model); 
        return Json(new { success = true });
    }


}
