using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers;

public class ProductController : AdminController
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;


    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = new CreateProductViewModel
        {
            CategorySelectListItems =
                await _categoryService.GetCategorySelectListItemsAsync()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.CategorySelectListItems =
                await _categoryService.GetCategorySelectListItemsAsync();
            return View(model);
        }

        await _productService.CreateAsync(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var product = await _productService.GetByIdWithDetailsAsync(id);
        if (product == null)
            return NotFound();

        var vm = new UpdateProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            OriginalPrice = product.OriginalPrice,
            Description = product.Description,
            AdditionalInformation = product.AdditionalInformation,
            ExistingImageUrl = product.ImageUrl,
            QuantityAvailable = product.QuantityAvailable,
            IsAvailable = product.IsAvailable,
            IsBestSeller = product.IsBestSeller,
            IsRated = product.IsRated,
            IsOnSale = product.IsOnSale,
            DiscountPercent = product.DiscountPercent,
            SaleStartDate = product.SaleStartDate,
            SaleEndDate = product.SaleEndDate,
            CategoryId = product.CategoryId,
            ExistingProductImages = product.ProductImages,
            CategorySelectListItems =
             await _categoryService.GetCategorySelectListItemsAsync()
                
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.CategorySelectListItems =
                await _categoryService.GetCategorySelectListItemsAsync();
            return View(model);
        }

        var result = await _productService.UpdateAsync(id, model);
        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return Ok();
    }
}
