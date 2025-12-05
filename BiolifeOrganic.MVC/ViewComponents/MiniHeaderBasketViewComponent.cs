using BiolifeOrganic.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.ViewComponents;

public class MiniHeaderBasketViewComponent:ViewComponent
{
    private readonly BasketManager _basketManager;
    public MiniHeaderBasketViewComponent(BasketManager basketManager)
    {
        _basketManager = basketManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var basket = await _basketManager.GetBasketAsync();

        return View("Default",basket);
    }
}
