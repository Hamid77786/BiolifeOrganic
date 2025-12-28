using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Logo;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers
{
    public class LogoController : AdminController
    {
        private readonly ILogoService  _logoService;
        private readonly FileService _fileService;


        public LogoController(ILogoService logoService, FileService fileService)
        {
            _logoService = logoService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var logos = await _logoService.GetAllAsync();

            return View(logos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLogoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.LogoFile != null && _fileService.IsImageFile(model.LogoFile))
            {
                model.LogoUrl = await _fileService.SaveFileAsync(model.LogoFile, "wwwroot/images/logos");
            }

            await _logoService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var logo = await _logoService.GetByIdAsync(id);
            if (logo == null) return NotFound();

            var updateModel = new UpdateLogoViewModel
            {
                Id = logo.Id,
                Name = logo.Name,
                ExistingLogoUrl = logo.LogoUrl
            };

            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateLogoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.NewLogoFile != null && _fileService.IsImageFile(model.NewLogoFile))
            {
                
                if (!string.IsNullOrEmpty(model.ExistingLogoUrl))
                    _fileService.DeleteFile("wwwroot/images/logos",model.ExistingLogoUrl);

                var savedFileName = await _fileService.SaveFileAsync(model.NewLogoFile, "wwwroot/images/logos");
                model.ExistingLogoUrl = savedFileName;


            }

            var success = await _logoService.UpdateAsync(id, model);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var logo = await _logoService.GetByIdAsync(id);

            if (logo != null && !string.IsNullOrEmpty(logo.LogoUrl))
                _fileService.DeleteFile("wwwroot/images/logos", logo.LogoUrl);

            var success = await _logoService.DeleteAsync(id);
            if (!success) return NotFound();

            return Ok();
        }


    }
}
