using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Header;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using System.Linq;

namespace BiolifeOrganic.Bll.Services;

public class HeaderManager : IHeaderService
{
    private readonly ICategoryService _categoryService;
    private readonly ILogoService _logoService;
    private readonly IWebContactService _webContactService;
    private readonly IWishlistRepository _wishlistRepository;
    private readonly BasketManager _basketManager;
    private readonly IMapper _mapper;

    public HeaderManager(BasketManager basketManager,IWishlistRepository wishlistRepository ,IWebContactService webContactService,ICategoryService categoryService,IProductService productService,ISliderService sliderService,ILogoService logoService,IMapper mapper)
    {
        _categoryService = categoryService;
        _logoService = logoService;
        _webContactService = webContactService;
        _wishlistRepository = wishlistRepository;
        _basketManager = basketManager;
        _mapper = mapper;
    }
    public async Task<HeaderViewModel> GetHeaderViewModelAsync()
    {
        var categories = await _categoryService.GetAllAsync(c => c.IsDeleted == false);
        var logos = await _logoService.GetAllAsync();
        var webContacts = (await _webContactService.GetAllAsync(c => c.IsDefault == true)).FirstOrDefault();
        var basketItems = await _basketManager.GetBasketAsync();
        var wishlistItems = await _wishlistRepository.GetAllAsync();

        var wishlistItemViewModels = wishlistItems
            .Select(w => _mapper.Map<WishlistItemViewModel>(w))
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