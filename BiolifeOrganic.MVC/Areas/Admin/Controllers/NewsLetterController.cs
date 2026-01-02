using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class NewsLetterController : AdminController
{
    private readonly INewsletterService _newsletterService;
    public NewsLetterController(INewsletterService newsletterService)
    {
        _newsletterService = newsletterService;
    }
    public async Task<IActionResult> Index()
    {
        var subscribers = await _newsletterService.GetAllSubscribersAsync();

        return View(subscribers);
    }
}
