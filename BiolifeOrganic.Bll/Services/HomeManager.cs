using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services;

public class HomeManager : IHomeService
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly ISliderService _sliderService;

    public HomeManager(ICategoryService categoryService, IProductService productService, ISliderService sliderService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _sliderService = sliderService;
    }
    public async Task<HomeViewModel> GetHomeViewModel()
    {
        var categories = await _categoryService.GetAllAsync(predicate: x => !x.IsDeleted);

        var products = (await _productService.GetAllAsync(
            predicate: x => !x.IsDeleted,
            include: query => query
                .Include(p => p.ProductImages!)
                .Include(c => c.Category!)
        )).Take(3).ToList();

        var sliders = await _sliderService.GetAllAsync();

        var homeViewModel = new HomeViewModel
        {
            Categories = categories.ToList(),
            Products = products.ToList(),
            Sliders = sliders.ToList()
        };

        return homeViewModel;

    }
}
