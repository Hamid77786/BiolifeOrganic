namespace BiolifeOrganic.Bll.ViewModels.Wishlist;

public class WishlistViewModel
{
    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
}
public class CreateWishlistViewModel
{
    public string? AppUserId { get; set; }
    public int ProductId { get; set; }
}
public class UpdateWishlistViewModel
{
    public int Id { get; set; }
    public string? AppUserId { get; set; }
    public int ProductId { get; set; }
}
