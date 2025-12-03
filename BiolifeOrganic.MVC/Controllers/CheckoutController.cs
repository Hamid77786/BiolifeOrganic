using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers;

public class CheckoutController : Controller
{
    public CheckoutController()
    {
        
    }
    public IActionResult Index()
    {
        return View();
    }
}
