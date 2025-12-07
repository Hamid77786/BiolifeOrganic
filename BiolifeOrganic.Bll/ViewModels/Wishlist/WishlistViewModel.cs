namespace BiolifeOrganic.Bll.ViewModels.Wishlist;

public class WishlistViewModel
{
    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
    public int? Count { get; set; }
    public List<WishlistItemViewModel> Items { get; set; } = [];

}
public class WishlistItemViewModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
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
