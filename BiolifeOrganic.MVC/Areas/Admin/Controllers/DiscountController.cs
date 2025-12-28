using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Discount;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers
{
    public class DiscountController : AdminController
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetAllAsync();
           
            return View(discounts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDiscountViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _discountService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            if (discount == null)
                return NotFound();

            var updateModel = new UpdateDiscountViewModel
            {
                Id = discount.Id,
                Code = discount.Code,
                Percentage = discount.Percentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                OnlyForNewUsers = discount.OnlyForNewUsers,
                OnlyForExistingUsers = discount.OnlyForExistingUsers,
                IsActive = discount.IsActive,
                MaxUsageCount = discount.MaxUsageCount,
            };

            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateDiscountViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _discountService.UpdateAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _discountService.DeleteAsync(id);
            return Ok();
        }

        public async Task<IActionResult> Details(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            if (discount == null)
                return NotFound();

            return View(discount);
        }
    }
}
