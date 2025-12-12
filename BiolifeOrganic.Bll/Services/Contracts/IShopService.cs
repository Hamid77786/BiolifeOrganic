using BiolifeOrganic.Bll.ViewModels.Review;
using BiolifeOrganic.Bll.ViewModels.Shop;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IShopService
{
    Task<ShopViewModel> GetShopViewModel(int productId,string? userId,int page, int pageSize);
    Task<List<ReviewViewModel>> GetRecentReviewsAsync(int take = 5);

}
