using BiolifeOrganic.Dll.ReadModels.Order;
using BiolifeOrganic.Dll.ReadModels.UserDiscount;
using BiolifeOrganic.Dll.ReadModels.Wislist;

namespace BiolifeOrganic.Dll.ReadModels.User;

public class UserRM
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? ProfileImagePath { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; }
    public int OrdersCount { get; set; }
    public int WishlistCount { get; set; }
    public int UserDiscountsCount { get; set; }

}

public class UserDetailsRM
{
    public string Id { get; set; } = null!;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; }

    public List<OrderDetailsRM> Orders { get; set; } = new();
    public List<WishlistRM> Wishlists { get; set; } = new();
    public List<UserDiscountRM> Discounts { get; set; } = new();
}








