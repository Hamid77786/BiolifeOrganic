namespace BiolifeOrganic.Dll.DataContext.Entities;

public class Wishlist: TimeStample
{
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
