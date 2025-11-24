using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
