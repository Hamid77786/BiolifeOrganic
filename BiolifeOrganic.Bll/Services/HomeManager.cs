using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Home;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiolifeOrganic.Bll.Services;

public class HomeManager : IHomeService
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly ISliderService _sliderService;
    private readonly IWishlistService _wishlistService;

    public HomeManager(IWishlistService wishlistService,ICategoryService categoryService, IProductService productService, ISliderService sliderService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _sliderService = sliderService;
        _wishlistService = wishlistService;
       
        
    }
    public async Task<HomeViewModel> GetHomeViewModel(string? userId)
    {
        var categories = await _categoryService.GetAllAsync(predicate: x => !x.IsDeleted);

        var products = (await _productService.GetAllAsync(
            predicate: x => !x.IsDeleted,
            include: query => query
                .Include(p => p.ProductImages!)
                .Include(c => c.Category!)
        )).ToList();

        if (!string.IsNullOrEmpty(userId))
        {
            var userWishlistIds = await _wishlistService.GetUserWishlistIdsAsync(userId);

            foreach (var p in products)
            {
                p.IsInWishlist = userWishlistIds.Contains(p.Id);
            }
        }

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
