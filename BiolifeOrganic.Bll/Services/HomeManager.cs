using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.Home;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Bll.ViewModels.Slider;
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
    private readonly IReviewService _reviewService;

    public HomeManager(IReviewService reviewService ,IWishlistService wishlistService,ICategoryService categoryService, IProductService productService, ISliderService sliderService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _sliderService = sliderService;
        _wishlistService = wishlistService;
        _reviewService = reviewService;
       
        
    }
    
    public async Task<HomeViewModel> GetHomeViewModel(string? userId, int? productId = null)
    {
        var categories = (await _categoryService.GetAllAsync(predicate: x => !x.IsDeleted))?.ToList() ?? new List<CategoryViewModel>();

        var products = (await _productService.GetAllAsync(
            predicate: x => !x.IsDeleted,
            include: query => query
                .Include(p => p.ProductImages!)
                .Include(c => c.Category!)
                .Include(r => r.Reviews!)
        ))?.ToList() ?? new List<ProductViewModel>();

        foreach (var product in products)
        {
            if (product.Reviews != null && product.Reviews.Any())
            {
                product.AverageStars = product.Reviews.Average(r => r.Stars);
                product.ReviewCount = product.Reviews.Count;
            }
            else
            {
                product.AverageStars = 0;
                product.ReviewCount = 0;
            }
        }

        var reviews = productId.HasValue
        ? (await _reviewService.GetByProductIdAsync(productId.Value))
        : new List<ReviewViewModel>();

        if (!string.IsNullOrEmpty(userId))
        {
            var userWishlistIds = await _wishlistService.GetUserWishlistIdsAsync(userId);

            foreach (var p in products)
            {
                p.IsInWishlist = userWishlistIds.Contains(p.Id);
            }
        }

        var sliders = (await _sliderService.GetAllAsync())?.ToList() ?? new List<SliderViewModel>();

        var homeViewModel = new HomeViewModel
        {
            Categories = categories,
            Products = products,
            Sliders = sliders,
            Reviews = reviews
        };

        return homeViewModel;
    }

}
