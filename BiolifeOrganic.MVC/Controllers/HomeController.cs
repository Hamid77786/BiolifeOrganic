using BiolifeOrganic.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BiolifeOrganic.MVC.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
