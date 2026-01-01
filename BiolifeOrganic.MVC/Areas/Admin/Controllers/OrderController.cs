using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class OrderController : AdminController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailsForUserAsync(id);
        if (order == null) return NotFound();

        return View(order); 
    }
}


