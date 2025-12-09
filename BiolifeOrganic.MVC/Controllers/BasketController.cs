using BiolifeOrganic.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly BasketManager _basketManager;

        public BasketController(BasketManager basketManager)
        {
            _basketManager = basketManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _basketManager.GetBasketAsync();

            return View(model);
        }

        public async Task<IActionResult> LoadMiniBasket()
        {
            var basket = await _basketManager.GetBasketAsync();

            return PartialView("_MiniBasketItemsPart", basket.Items);
        }
        
        public async Task<IActionResult> LoadBasket()
        {
            var basket =await _basketManager.GetBasketAsync();

            return PartialView("_BasketItemsPart",basket.Items);
        }

        public async Task<IActionResult> LoadBasketBlock()
        {
            var basket = await _basketManager.GetBasketAsync();
            return PartialView("_BasketBlock", basket);
        }

        public async Task<IActionResult> LoadHeaderBasket()
        {
            var basket = await _basketManager.GetBasketAsync();
            return PartialView("_BasketHeader", basket);
        }
        
        [HttpPost]




        [HttpPost]
        public async Task<IActionResult> AddNew(int id, int qty = 1)
        {
            if (qty < 1) qty = 1;
            if (qty > 5) qty = 5;

            _basketManager.AddToBasketNew(id, qty);

            var basket = await _basketManager.GetBasketAsync();

            return Json(new { success = true, count = basket.TotalCount });
        }



        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            _basketManager.AddToBasket(id);

            var basket = await _basketManager.GetBasketAsync();

            return Json(new { success = true, count = basket.TotalCount });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            _basketManager.RemoveFromBasket(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAll()
        {
            _basketManager.RemoveAllFromBasket();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQty(int id, int qty)
        {
            if (qty < 1) qty = 1;
            if (qty > 5) qty = 5;

            var basket = await _basketManager.ChangeQuantityAsync(id, qty);
            return Json(new
            {
                success = true,
                totalCount = basket.TotalCount,
                totalPrice = basket.TotalPrice
            });
        }
    }
}
