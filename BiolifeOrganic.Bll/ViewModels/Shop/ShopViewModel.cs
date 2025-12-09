using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Review;

namespace BiolifeOrganic.Bll.ViewModels.Shop;

public class ShopViewModel
{
    public List<ReviewViewModel> Reviews { get; set; } = [];
    public List<ProductViewModel> Products { get; set; } = [];
    public ProductViewModel? Product { get; set; }
    public ReviewViewModel? Review { get; set; }
    public Dictionary<int, int> StarCounts { get; set; } = [];
    public int TotalReviews { get; set; }

}
