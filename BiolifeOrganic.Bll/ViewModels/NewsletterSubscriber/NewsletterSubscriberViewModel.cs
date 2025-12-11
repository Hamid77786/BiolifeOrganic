namespace BiolifeOrganic.Bll.ViewModels.NewsletterSubscriber;

public class NewsletterSubscriberViewModel
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime SubscribedAt { get; set; }
}


