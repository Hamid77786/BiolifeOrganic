using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class FooterViewComponent: ViewComponent
{
    private readonly IFooterService _footerService;
    public FooterViewComponent(IFooterService footerService)
    {
        _footerService = footerService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var footerVm = await _footerService.GetFooterViewModelAsync();
        
        return View(footerVm);
    }
}
