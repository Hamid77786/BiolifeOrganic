using BiolifeOrganic.Bll.ViewModels.Pagination;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Review;

namespace BiolifeOrganic.Bll.ViewModels.Shop;

public class ShopViewModel
{
    public List<ReviewViewModel> Reviews { get; set; } = [];
    public List<ReviewViewModel> PagedReviews { get; set; } = [];
    public List<ProductViewModel> Products { get; set; } = [];
    public List<ProductViewModel> PagedProducts { get; set; } = [];
    public ProductViewModel? Product { get; set; }
    public Dictionary<int, int> StarCounts { get; set; } = [];
    public int TotalReviews { get; set; }
    public int TotalProducts { get; set; }
    public string? PriceFilter { get; set; }
    public string? AvailableFilter {  get; set; }
    public PaginationViewModel? Pagination { get; set; }
    public List<ProductViewModel> RelatedProducts { get; set; } = [];

}
