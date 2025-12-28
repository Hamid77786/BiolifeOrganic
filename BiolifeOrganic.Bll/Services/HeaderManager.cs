using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Header;
using BiolifeOrganic.Dll.DataContext.Entities;
using System.Linq;

namespace BiolifeOrganic.Bll.Services;

public class HeaderManager : IHeaderService
{
    private readonly ICategoryService _categoryService;
    private readonly ILogoService _logoService;
    private readonly IWebContactService _webContactService;
    private readonly IWishlistService _wishlistService;
    private readonly BasketManager _basketManager;

    public HeaderManager(BasketManager basketManager,IWishlistService wishlistService ,IWebContactService webContactService,ICategoryService categoryService,IProductService productService,ISliderService sliderService,ILogoService logoService)
    {
        _categoryService = categoryService;
        _logoService = logoService;
        _webContactService = webContactService;
        _wishlistService = wishlistService;
        _basketManager = basketManager;
    }
    public async Task<HeaderViewModel> GetHeaderViewModelAsync()
    {
        var categories = await _categoryService.GetAllAsync(c => c.IsDeleted == false);
        var logos = await _logoService.GetAllAsync();
        var webContacts = (await _webContactService.GetAllAsync(c => c.IsDefault == true)).FirstOrDefault();
        var basketItems = await _basketManager.GetBasketAsync();
        var wishlistItems = await _wishlistService.GetAllAsync();

        var wishlistItemViewModels = wishlistItems
            .SelectMany(wishlist => wishlist.Items)
            .ToList();

        var headerModel = new HeaderViewModel
        {
            Categories = categories.ToList(),
            Logos = logos.ToList(),
            WebContacts = webContacts,
            Items = basketItems.Items,
            WishItems = wishlistItemViewModels
        };

        return headerModel;
    }
}