using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    public class ContactController : Controller
    {
        public ContactController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
