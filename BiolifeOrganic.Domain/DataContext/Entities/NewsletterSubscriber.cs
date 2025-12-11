using System.ComponentModel.DataAnnotations;

namespace BiolifeOrganic.Dll.DataContext.Entities;

public class NewsletterSubscriber:Entity
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}
