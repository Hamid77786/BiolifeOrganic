using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class OrderController : AdminController
{
    private readonly IOrderService _orderService;
    private readonly IContactService _contactService;

    public OrderController(IOrderService orderService, IContactService contactService)
    {
        _orderService = orderService;
        _contactService = contactService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailsForUserAsync(id);
        if (order == null) return NotFound();

        return View(order); 
    }

    [HttpGet]
    public async Task<IActionResult> EditAddress(int orderId)
    {
        var order = await _orderService.GetOrderDetailsForAdminAsync(orderId);
        if (order == null) return NotFound();



        var model = new UpdateContactViewModel
        {
            OrderId = order.Id,


            Id = order.ShippingContact.Id,
            FirstName = order.ShippingContact.FirstName,
            LastName = order.ShippingContact.LastName,
            Address = order.ShippingContact.Address,
            City = order.ShippingContact.City,
            Country = order.ShippingContact.Country,
            PostalCode = order.ShippingContact.PostalCode,
            PhoneNumber = order.ShippingContact.Phone,
            Email = order.ShippingContact.Email
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditAddress(UpdateContactViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _contactService.UpdateAddressAsync(model.Id, null, model);

        return RedirectToAction("Details", new { id = model.OrderId });
    }


}


