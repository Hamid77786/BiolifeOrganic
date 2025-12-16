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

    public ShopManager(IProductService productService, IWishlistService wishlistService, IReviewService reviewService)
    {
        _productService = productService;
        _wishlistService = wishlistService;
        _reviewService = reviewService;
    }

    public async Task<List<ReviewViewModel>> GetRecentReviewsAsync(int take = 10)
    {
        var reviews = (await _reviewService.GetAllAsync(
         predicate: null,
         include: q => q.Include(r => r.Product!).Include(r => r.AppUser!),
         orderBy: q => q.OrderByDescending(r => r.PostedDate),
         AsNoTracking: true
         ));




        return reviews.Take(take).ToList();
    }


    public async Task<ShopViewModel> GetShopViewModel(int productId, string? userId, int page = 1, int pageSizeRev = 2)
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
         ? reviews.Skip((pageNumber - 1) * pageSizeRev).Take(pageSizeRev).ToList()
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
                TotalPagesReviews = (int)Math.Ceiling(totalReviews / (double)pageSizeRev),
            }

        };

        return shopViewModel;
    }

    public async Task<ShopViewModel> GetShopAsync(int page, int pageSize, string? priceFilter, string? availabilityFilter)
    {
        int pageNumber = page < 1 ? 1 : page;

        var query = _productService.GetProductsQuery(
            priceFilter,
            availabilityFilter
        );

        int totalProducts = await query.CountAsync();

        var pagedProducts = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category != null ? p.Category.Name : null,
                Description = p.Description,
                OriginalPrice = p.OriginalPrice,
                DiscountedPrice = p.DiscountedPrice,
                
                IsOnSale = p.IsOnSale
            })
            .ToListAsync();

        return new ShopViewModel
        {
            PagedProducts = pagedProducts,
            TotalProducts = totalProducts,
            PriceFilter = priceFilter,
            AvailableFilter = availabilityFilter,
            Pagination = new PaginationViewModel
            {
                CurrentPage = pageNumber,
                TotalPagesProducts =
                    (int)Math.Ceiling(totalProducts / (double)pageSize)
            }
        };

    }
}















