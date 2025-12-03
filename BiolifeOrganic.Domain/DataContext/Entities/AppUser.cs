using Microsoft.AspNetCore.Identity;

namespace BiolifeOrganic.Dll.DataContext.Entities;

public class AppUser:IdentityUser
{
    
   
    public string? FullName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Contact> Contacts { get; set; } = [];
    public List<Wishlist> Wishlists { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public List<Promocode> Promocodes { get; set; } = [];





}
