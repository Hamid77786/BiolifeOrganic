using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.NewsletterSubscriber;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    
    public class NewsLetterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsLetterController(INewsletterService newsletter)
        {
            _newsletterService = newsletter;
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] NewsletterSubscriberViewModel dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return Json(new { success = false, message = "Email is required" });

            try
            {
                await _newsletterService.SubscribeAsync(dto.Email);
                return Json(new { success = true, message = "You have successfully subscribed!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

       


    }
}
