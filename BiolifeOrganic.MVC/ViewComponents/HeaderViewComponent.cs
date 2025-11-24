using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class HeaderViewComponent:ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
