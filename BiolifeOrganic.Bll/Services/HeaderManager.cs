using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Header;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services;

public class HeaderManager : IHeaderService
{
    private readonly ICategoryService _categoryService;
    private readonly ILogoService _logoService;
    private readonly IWebContactService _webContactService;
    private readonly BasketManager _basketManager;

    public HeaderManager(BasketManager basketManager ,IWebContactService webContactService,ICategoryService categoryService,IProductService productService,ISliderService sliderService,ILogoService logoService)
    {
        _categoryService = categoryService;
        _logoService = logoService;
        _webContactService = webContactService;
        _basketManager = basketManager;
    }
    public async Task<HeaderViewModel> GetHeaderViewModelAsync()
    {
        var categories = await _categoryService.GetAllAsync(c => c.IsDeleted == false);
        var logos = await _logoService.GetAllAsync();
        var webContacts = await _webContactService.GetAllAsync(c=>c.IsDefault==true);
        var basketItems = await _basketManager.GetBasketAsync();

       
       

        var headerModel = new HeaderViewModel
        {
            Categories = categories.ToList(),
            Logos = logos.ToList(),
            WebContacts = webContacts.ToList(),
            Items = basketItems.Items,

        };

        return headerModel;
    }
}