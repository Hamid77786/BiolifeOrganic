namespace BiolifeOrganic.Dll.ReadModels.Wislist;

public class WishlistItemRM
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}

public class WishlistRM
{
    public string? AppUserId { get; set; }
    public string? AppUserName { get; set; }
    public int Count { get; set; }
    public List<WishlistItemRM> Items { get; set; } = new();
}
