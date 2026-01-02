using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Bll.ViewModels.User;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class UserController : AdminController
{
    private readonly IUserService _userService;
    private readonly IContactService _contactService;
    private readonly UserManager<AppUser> _userManager;
    public UserController(IContactService contactService,UserManager<AppUser> userManager, IUserService userService)
    {
        _userService = userService;
        _userManager = userManager;
        _contactService = contactService;
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
        var currentUserId = _userManager.GetUserId(User);
        if (currentUserId == null) return NotFound();

        var result = await _userService.DeleteUserAsync(id, currentUserId);

        return Json(new
        {
            success = result.Success,
            errorMessage = result.ErrorMessage
        });
    }

    [HttpGet]
    public async Task<IActionResult> Deleted()
    {
        var users = await _userService.GetDeletedUsersAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(string id)
    {
        var success = await _userService.RestoreUserAsync(id);

        return Json(new
        {
            success,
            message = success ? null : "Restore failed"
        });
    }

    [HttpGet]
    public async Task<IActionResult> Contacts(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest();

        var contacts = await _contactService.GetUserAddressesAsync(userId);

        ViewBag.UserId = userId;
        return View(contacts);
    }

   












}
