using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

[Authorize(Roles ="Admin")]
[Area ("Admin")]
public class DashboardController : Controller
{
   
   public IActionResult Index()
    {
        return View();
    }
   
}