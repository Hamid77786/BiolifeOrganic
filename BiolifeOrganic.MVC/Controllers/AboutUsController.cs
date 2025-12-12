using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers;

public class AboutUsController : Controller
{
    private readonly IShopService _shopService;
    public AboutUsController(IShopService shopService)
    {
      _shopService = shopService;  
    }
    public async Task<IActionResult> Index()
    {
        var reviews = await _shopService.GetRecentReviewsAsync(10);

        return View(reviews);
    }
}
