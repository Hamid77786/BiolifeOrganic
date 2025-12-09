using BiolifeOrganic.Bll.ViewModels.Shop;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IShopService
{
    Task<ShopViewModel> GetShopViewModel(int productId,string? userId);
}
