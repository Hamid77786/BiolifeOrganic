using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class CategoryController : AdminController
{
    private readonly ICategoryService _categoryService;
    private readonly FileService _fileService;

    public CategoryController(ICategoryService categoryService, FileService fileService)
    {
        _categoryService = categoryService;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
       
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.ImageFile != null)
        {
            if (!_fileService.IsImageFile(model.ImageFile))
            {
                ModelState.AddModelError("ImageFile", "File must be an image!");
                return View(model);
            }

            if (model.ImageFile.Length > 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "Image file size must be 1MB max!");
                return View(model);
            }

            model.ImageUrl = _fileService.GetFileUrl("images/categories", await _fileService.SaveFileAsync(model.ImageFile, "wwwroot/images/categories"));
        }

        await _categoryService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();

        var updateModel = new UpdateCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl,
            CategoryIcon = category.CategoryIcon
        };

        return View(updateModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.ImageFile != null)
        {
            if (!_fileService.IsImageFile(model.ImageFile))
            {
                ModelState.AddModelError("ImageFile", "File must be an image!");
                return View(model);
            }

            if (model.ImageFile.Length > 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "Image file size must be 1MB max!");
                return View(model);
            }

            model.ImageUrl = _fileService.GetFileUrl("images/categories", await _fileService.SaveFileAsync(model.ImageFile, "wwwroot/images/categories"));
        }

        var result = await _categoryService.UpdateAsync(id, model);
        if (!result)
            return BadRequest();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        if (!result) return NotFound();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _categoryService.GetCategoryWithProductsAsync(id);
        
        if (category == null) return NotFound();

        return View(category);
    }

}
