using Microsoft.AspNetCore.Identity;

namespace BiolifeOrganic.Dll.DataContext.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? ProfileImagePath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    public List<Contact> Contacts { get; set; } = new();
    public List<Wishlist> Wishlists { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
    public List<Review> Reviews { get; set; } = new();
    public List<UserDiscount> UserDiscounts { get; set; } = new();
}
