using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class HeaderViewComponent:ViewComponent
{
    private readonly IHeaderService _headerService;

    public HeaderViewComponent(IHeaderService headerService)
    {
        _headerService = headerService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var headerViewModel = await _headerService.GetHeaderViewModelAsync();

        return View(headerViewModel);
    }
}
