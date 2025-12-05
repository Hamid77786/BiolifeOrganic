using BiolifeOrganic.Bll.Services;
using Microsoft.AspNetCore.Mvc;


namespace BiolifeOrganic.MVC.ViewComponents
{
    public class MiniBasketViewComponent:ViewComponent
    {
        private readonly BasketManager _basketmanager;

        public MiniBasketViewComponent(BasketManager basketmanager)
        {
            _basketmanager = basketmanager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketVm = await _basketmanager.GetBasketAsync();
            
            return View("Default", basketVm);
        }
    }
}
