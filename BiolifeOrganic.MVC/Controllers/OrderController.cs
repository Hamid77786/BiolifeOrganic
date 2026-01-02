using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BiolifeOrganic.MVC.Controllers;

[Authorize(Roles ="User")]

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<AppUser> _userManager;

    public OrderController(UserManager<AppUser> userManager,IOrderService orderService)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Account");

        var orders = await _orderService.GetUserOrdersAsync(user.Id);
        
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Account");

        var order = await _orderService.GetOrderDetailsAsync(id, user.Id);

        if (order == null)
        {
            TempData["ErrorMessage"] = "Order not found.";
            return RedirectToAction(nameof(Index));
        }

        return PartialView("_OrderItemDetails", order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Json(new { success = false, message = "Unauthorized" });

        try
        {
            var result = await _orderService.DeleteUserOrderAsync(id, user.Id);

            if (!result)
                return Json(new { success = false, message = "Order not found" });

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditShipping(int orderId)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var model = await _orderService.GetShippingContactForEditAsync(orderId, userId);

        if (model == null)
            return NotFound();

        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditShipping(UpdateContactViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        try
        {
            await _orderService.UpdateShippingAddressAsync(model, userId);
            TempData["Success"] = "Shipping address updated successfully";
            return RedirectToAction("Index"); 
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }












}
