using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    public class AboutUsController : Controller
    {
        public AboutUsController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
