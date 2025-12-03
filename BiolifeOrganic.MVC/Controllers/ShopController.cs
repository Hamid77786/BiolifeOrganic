using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers;


public class ShopController : Controller
{
    public ShopController()
    {
        
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Detail()
    {
        return View();
    }
}
