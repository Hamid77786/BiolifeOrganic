using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class NewsletterManager : INewsletterService
{
    private readonly INewsletterSubscriberRepository _newsletterRepository;
    private readonly IEmailService _emailService;

    public NewsletterManager(INewsletterSubscriberRepository newsletter, IEmailService emailService)
    {
       _emailService = emailService;
        _newsletterRepository = newsletter;
    }
    public async Task SendPromotionEmailAsync(string subject, string htmlMessage)
    {
        var subscribers = await _newsletterRepository.GetAllAsync();

        foreach (var sub in subscribers)
        {
            await _emailService.SendEmailAsync(sub.Email, subject, htmlMessage);
        }
    }

    public async Task SubscribeAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        var exists = await _newsletterRepository.GetAsync(s => s.Email == email);
        if (exists != null)
            return;

        var subscriber = new NewsletterSubscriber
        {
            Email = email,
            SubscribedAt = DateTime.UtcNow
        };

        await _newsletterRepository.CreateAsync(subscriber);

       
        await _emailService.SendEmailAsync(
            email,
            "Welcome to Biolife Organic!",
            "<h2>You are successfully subscribed!</h2><p>You will now receive updates about discounts and new products.</p>"
        );
    }
}
