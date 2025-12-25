using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers;

[Authorize]

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

    


}
