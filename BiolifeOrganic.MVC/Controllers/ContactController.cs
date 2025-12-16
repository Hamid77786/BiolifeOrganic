using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IAdminContactRepository _adminContactRepository;
        private readonly IWebContactService _webContactService;
        public ContactController(IWebContactService webContactService, IEmailService emailService, IAdminContactRepository adminContact)
        {
            _emailService = emailService;
            _adminContactRepository = adminContact;
            _webContactService = webContactService;
        }
        public async Task<IActionResult> Index()
        {
            var webContactsVm = await _webContactService.GetAsync(c => c.IsDefault == true);

            return View(webContactsVm);
        }

        [HttpPost] 
        public async Task<IActionResult> Send(ContactViewModel model)
        {
            if (ModelState.IsValid) return View(model);

            var adminContact = (await _adminContactRepository.GetAllAsync()).Select(x => x.Email).FirstOrDefault();

            if (adminContact==null||string.IsNullOrWhiteSpace(adminContact))
                return BadRequest("Admin email not configured");


            var subject = "New contact message";
            var body = $@"
            Name: {model.LastName}
            Email: {model.Email}
            Phone: {model.Phone}

            Message:
            {model.Message}
            ";

            await _emailService.SendEmailAsync(adminContact, subject, body);

            return RedirectToAction("Index");

        }
    }
}
