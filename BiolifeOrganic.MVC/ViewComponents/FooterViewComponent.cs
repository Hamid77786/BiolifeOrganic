using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class FooterViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
