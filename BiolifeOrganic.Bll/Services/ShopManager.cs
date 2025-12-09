using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Shop;
using Microsoft.EntityFrameworkCore;


namespace BiolifeOrganic.Bll.Services;

public class ShopManager : IShopService
{
    private readonly IProductService _productService;
    private readonly IWishlistService _wishlistService;
    private readonly IReviewService _reviewService;

    public ShopManager(IProductService productService,IWishlistService wishlistService,IReviewService reviewService)
    {
        _productService = productService;
        _wishlistService = wishlistService;
        _reviewService = reviewService;
    }

    public async Task<ShopViewModel> GetShopViewModel(int productId,string? userId)
    {
        var products = (await _productService.GetAllAsync(
            predicate: x => !x.IsDeleted,
            include: query => query
                .Include(p => p.ProductImages!)
                .Include(c => c.Category!)
                .Include(r => r.Reviews!)
        )).ToList();
        var product = products.FirstOrDefault(x => x.Id == productId);

        var reviews = await _reviewService.GetByProductIdAsync(productId);

        Dictionary<int, int> starCounts = new();
        int totalReviews = 0;

        if (reviews != null && reviews.Any())
        {
            starCounts = reviews
                .GroupBy(r => r.Stars)
                .ToDictionary(g => g.Key, g => g.Count());

            totalReviews = starCounts.Values.Sum();
        }


        if (!string.IsNullOrEmpty(userId))
        {
            var userWishlistIds = await _wishlistService.GetUserWishlistIdsAsync(userId);

            foreach (var p in products)
            {
                p.IsInWishlist = userWishlistIds.Contains(p.Id);


                if (product != null)
                    product.IsInWishlist = userWishlistIds.Contains(product.Id);
            }
            if (product == null)
            {
                return new ShopViewModel
                {
                    Products = products,
                    Reviews = reviews!,
                    Product = null,
                    StarCounts = starCounts,
                    TotalReviews = totalReviews
                };
            }

        }

        var shopViewModel = new ShopViewModel
        {
            Products = products.ToList(),
            Reviews = reviews,
            Product = product
        };
           
        return shopViewModel;

    }






}
