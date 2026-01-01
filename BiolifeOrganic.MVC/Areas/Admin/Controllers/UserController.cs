using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class UserController : AdminController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IActionResult> Index()
    {
        var user = await _userService.GetAllUsersWithDetailsAsync();


        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleBlock(string id)
    {
        await _userService.ToggleBlockAsync(id);
        return Ok();
    }

    public async Task<IActionResult> Details(string id)
    {
        var model = await _userService.GetUserDetailsAsync(id);
        if (model == null) return NotFound();

        return View(model);
    }




}
