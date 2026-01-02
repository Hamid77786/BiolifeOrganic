using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.Dll.DataContext.Entities;

public class NewsletterSubscriber:TimeStample
{

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}
