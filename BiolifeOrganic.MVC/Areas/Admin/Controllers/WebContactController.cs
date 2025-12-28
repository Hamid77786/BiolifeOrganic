using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.SetDefaultForWebCon;
using BiolifeOrganic.Bll.ViewModels.WebContact;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Areas.Admin.Controllers
{
    public class WebContactController : AdminController
    {
        private readonly IWebContactService _webContactService;
        public WebContactController(IWebContactService webContactService)
        {
             _webContactService = webContactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _webContactService.GetAllAsync();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWebContactViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _webContactService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var contact = await _webContactService.GetByIdAsync(id);
            if (contact == null) return NotFound();

            var updateModel = new UpdateWebContactViewModel
            {
                Id = contact.Id,
                EmailAgree = contact.EmailAgree,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address,
                Schedule = contact.Schedule,
                ContactOption = contact.ContactOption
            };

            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateWebContactViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var result = await _webContactService.UpdateAsync(id, model);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _webContactService.DeleteAsync(id);
            if (!result) return NotFound();

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDefault([FromBody]SetDefaultContactDto dto)
        {
           var result = await _webContactService.SetDefaultContactAsync(dto.Id, dto.Option);
            if (!result) return NotFound();

            return Ok();
        }

        public async Task<IActionResult> ByOption(ContactOption? option)
        {
            if (option == null)
            {
                var all = await _webContactService.GetAllAsync();
                return View("Index", all);
            }

            var contacts = await _webContactService.GetByOptionAsync(option.Value);
            return View("Index", contacts);
        }
    }
}
