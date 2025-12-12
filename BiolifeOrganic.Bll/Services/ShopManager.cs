using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Pagination;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Review;
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
    public async Task<List<ReviewViewModel>> GetRecentReviewsAsync(int take = 10)
    {
        var reviews = (await _reviewService.GetAllAsync(
         predicate: null,
         include: q => q.Include(r => r.Product!),
         orderBy: q => q.OrderByDescending(r => r.PostedDate)
         )).ToList();


        var topReviews = reviews.Take(take)
            .Select(r => new ReviewViewModel
            {
                Id = r.Id,
                Name = r.Name,
                EmailAddress = r.EmailAddress,
                Note = r.Note,
                Stars = r.Stars,
                ProductId = r.ProductId,
                ProductName = r.ProductName,
                PostedDate = r.PostedDate,
                AppUserId = r.AppUserId,
                PhotoPath = r.PhotoPath
            })
            .ToList();

        return topReviews;
    }

    public async Task<ShopViewModel> GetShopViewModel(int productId, string? userId, int page = 1, int pageSize = 2)
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

        int pageNumber = page < 1 ? 1 : page;

        int totalReviews = reviews?.Count ?? 0;

        var pagedReviews = (reviews != null && reviews.Any())
         ? reviews.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
         : new List<ReviewViewModel>();

        
        var starCounts = reviews?
            .GroupBy(r => r.Stars)
            .ToDictionary(g => g.Key, g => g.Count())
            ?? new Dictionary<int, int>();

        List<ProductViewModel> relatedProducts = new();

        if (product != null)
        {
            relatedProducts = products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(5)
                .ToList();
        }


        if (!string.IsNullOrEmpty(userId))
        {
            var userWishlistIds = await _wishlistService.GetUserWishlistIdsAsync(userId);
            foreach (var p in products)
                p.IsInWishlist = userWishlistIds.Contains(p.Id);

            if (product != null)
                product.IsInWishlist = userWishlistIds.Contains(product.Id);
        }

        var shopViewModel = new ShopViewModel
        {
            Products = products,
            RelatedProducts = relatedProducts,
            Product = product,
            Reviews = reviews ?? new List<ReviewViewModel>(),
            PagedReviews = pagedReviews,
            StarCounts = starCounts,
            TotalReviews = totalReviews,
            Pagination = new PaginationViewModel
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalReviews / (double)pageSize)
            }
        };

        return shopViewModel;
    }







}
