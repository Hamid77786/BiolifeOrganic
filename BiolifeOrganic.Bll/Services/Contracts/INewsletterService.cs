using BiolifeOrganic.Bll.ViewModels.NewsletterSubscriber;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface INewsletterService
{
    Task SubscribeAsync(string email);
    Task SendPromotionEmailAsync(string subject, string htmlMessage);
}
