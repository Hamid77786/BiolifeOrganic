using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;


public class DashboardController : AdminController
{
   
   public IActionResult Index()
    {
        return View();
    }
   
}