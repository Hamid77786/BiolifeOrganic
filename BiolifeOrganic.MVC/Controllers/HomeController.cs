using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BiolifeOrganic.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(UserManager<AppUser> usermanager, IHomeService homeService)
        {
            _homeService = homeService;
            _userManager = usermanager;

        }

        public async Task<IActionResult> Index(int productId)
        {
            var userId = _userManager.GetUserId(User);
            var homeViewModel = await _homeService.GetHomeViewModel(userId,productId);

            return View(homeViewModel);
        }


    }
}
