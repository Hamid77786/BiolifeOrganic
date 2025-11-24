using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers;

public class ShoppingCart : Controller
{
    public ShoppingCart()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
