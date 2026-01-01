using BiolifeOrganic.Bll.Constants;
using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers
{
    public class SliderController : AdminController
    {
        private readonly ISliderService _sliderService;
        private readonly FileService _fileService;
        public SliderController(ISliderService sliderService, FileService fileService)
        {
            _sliderService = sliderService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSliderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Image != null && _fileService.IsImageFile(model.Image))
            {
                model.ImageUrl = await _fileService
                    .SaveFileAsync(model.Image, FilePathConstants.SliderImagePath);
            }

           

            await _sliderService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null) return NotFound();

            var vm = new UpdateSliderViewModel
            {
                Id = slider.Id,
                HtmlContent = slider.HtmlContent,
                ExistingImageUrl = slider.ImageUrl,
                DisplayOrder = slider.DisplayOrder,
                IsActive = slider.IsActive,
                ButtonLink = slider.ButtonLink,
                ButtonText = slider.ButtonText,
                Description = slider.Description,
                SubTitle = slider.SubTitle,
                Title = slider.Title,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateSliderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string? imageUrl = model.ExistingImageUrl;


            if (model.Image != null && _fileService.IsImageFile(model.Image))
            {
                if (!string.IsNullOrEmpty(model.ExistingImageUrl))
                    _fileService.DeleteFile(FilePathConstants.SliderImagePath,
                                            model.ExistingImageUrl);

                imageUrl = await _fileService
                    .SaveFileAsync(model.Image, FilePathConstants.SliderImagePath);
            }

            var slider = new Slider
            {
                Id = model.Id,
                HtmlContent = model.HtmlContent,
                ImageUrl = imageUrl, 
                DisplayOrder = model.DisplayOrder,
                IsActive = model.IsActive,
                Title = model.Title,
                SubTitle = model.SubTitle,
                Description = model.Description,
                ButtonText = model.ButtonText,
                ButtonLink = model.ButtonLink
            };




            var result = await _sliderService.UpdateAsync(id, model);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sliderService.DeleteAsync(id);
            if (!result) return NotFound();

            return Ok();
        }

    }
}
