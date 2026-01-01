using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.User;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class UserController : AdminController
{
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _userManager;
    public UserController(UserManager<AppUser> userManager, IUserService userService)
    {
        _userService = userService;
        _userManager = userManager;
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

    [HttpPost]
    public async Task<IActionResult> ToggleAdminRole(string id)
    {
        await _userService.ToggleAdminRoleAsync(id);
        return Ok();
    }


    public async Task<IActionResult> Details(string id)
    {
        var model = await _userService.GetUserDetailsAsync(id);
        if (model == null) return NotFound();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }

   
    





}
