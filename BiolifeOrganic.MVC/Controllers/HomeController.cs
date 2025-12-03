using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BiolifeOrganic.MVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IHomeService _homeService;

        //public HomeController(IHomeService homeService)
        //{
        //    _homeService = homeService;
        //}

        public async Task<IActionResult> Index()
        {
            //var homeViewModel = await _homeService.GetHomeViewModel();

            return View();
        }

       
    }
}
