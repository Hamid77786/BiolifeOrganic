using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using Microsoft.AspNetCore.Authorization;

namespace BiolifeOrganic.MVC.Controllers;

public class CheckoutController : Controller
{
    private readonly ICheckoutService _checkoutService;
    private readonly UserManager<AppUser> _userManager;
    private readonly BasketManager _basketManager;

    public CheckoutController(BasketManager basketManager, ICheckoutService checkoutService, UserManager<AppUser> userManager)
    {
        
        _checkoutService = checkoutService;
        _userManager = userManager;
        _basketManager = basketManager;
        
    }

        
    public async Task<IActionResult> Index()
    {
        var model = await _checkoutService.BuildCheckoutViewModelAsync(User);

        if (model == null)
        {
            TempData["Error"] = "Your cart is empty!";
            return RedirectToAction("Index", "Home");
        }
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    
    public async Task<IActionResult> Index(CheckoutViewModel model)
    {
        model.IsGuest = !User.Identity!.IsAuthenticated;

        if (!ModelState.IsValid)
            return View(model);

        var result = await _checkoutService.ProcessCheckoutAsync(User, model);

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Error ?? "Checkout failed");
            return View(model);
        }

        TempData["Success"] = "Your order has been placed successfully!";
        return RedirectToAction("Index", "Order");
    }

    //[HttpPost]
    //public async Task<IActionResult> ApplyDiscount([FromBody] DiscountRequestVM request)
    //{
    //    if (string.IsNullOrWhiteSpace(request.Code))
    //    {
    //        return Json(new { success = false, message = "Please enter a discount code" });
    //    }

    //    var userId = User.Identity!.IsAuthenticated
    //    ? _userManager.GetUserId(User)
    //    : null;

    //    var basket = await _basketManager.GetBasketAsync(); 
    //    var totalAmount = basket?.Items.Sum(x => x.Price * x.Quantity) ?? 0;

    //    var result = await _discountService.ValidateAsync(request.Code, userId, totalAmount);

    //    if (!result.IsValid)
    //        return Json(new { success = false, message = "Invalid discount code" });

    //    return Json(new { success = true, discount = result.Percentage });
    //}





}
