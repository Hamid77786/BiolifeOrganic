using BiolifeOrganic.Bll.Constants;
using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BiolifeOrganic.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly FileService _fileService;
        public AccountController(FileService fileService, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager,IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _fileService = fileService;

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? profileFileName = null;

            if (model.Photo != null && _fileService.IsImageFile(model.Photo))
            {
                profileFileName = await _fileService.SaveFileAsync(model.Photo, FilePathConstants.ProfileImagePath);
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                ProfileImagePath = profileFileName 
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);
            }
            await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account",
                             new { userId = user.Id, token }, Request.Scheme);

            await _emailService.SendEmailAsync(user.Email!,
                   "Confirm your email",
                   $"<p>Thank you for registering! Please confirm your email by clicking the link below:</p>" +
                   $"<a href='{confirmationLink}'>Confirm Email</a>");


            return RedirectToAction("Index", "Home");

        }
        
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Invalid email confirmation request.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Email confirmed. Please log in.";
                return RedirectToAction(nameof(Login));
            }

            TempData["ErrorMessage"] = "Email confirmation failed.";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return View(model);

            bool hasExist = await _userManager.CheckPasswordAsync(user,model.Password);
            if (!hasExist) 
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false, lockoutOnFailure: false);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", $"You are banned {user.LockoutEnd!.Value.AddHours(4).ToString()}");

                return View(model);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is wrong!.");

                return View(model);
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var username = User.Identity!.Name ?? "";

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest();
            }
            bool isExist = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);

            if (isExist)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return BadRequest(model);
                }
            }

            return RedirectToAction(nameof(Login));
        }
           

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email not found");
                return View(model);
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);


            var resetLink = Url.Action("ResetPassword", "Account",
                new { email = model.Email, resetPasswordToken = resetToken },
                Request.Scheme, Request.Host.ToString());

            await _emailService.SendEmailAsync(user.Email!,
                "Reset your password",
                $"<a href='{resetLink}'>Click here to reset your password</a>");


            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string resetPasswordToken)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(resetPasswordToken))
            {
                return BadRequest("Invalid password reset link.");
            }


            var model = new ResetPasswordViewModel
            {
                Email = email,
                ResetPasswordToken = resetPasswordToken,
                Password = string.Empty,
                ConfirmPassword = string.Empty


            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.ResetPasswordToken, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            TempData["SuccessMessage"] = "Password successfully reset. Please log in.";

            return RedirectToAction(nameof(Login));
        }


    }
}
           











