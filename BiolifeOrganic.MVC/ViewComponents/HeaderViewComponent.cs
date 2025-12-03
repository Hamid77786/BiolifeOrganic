using BiolifeOrganic.Bll.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class HeaderViewComponent:ViewComponent
{

    public HeaderViewComponent()
    {
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        

        return View();
    }
}
