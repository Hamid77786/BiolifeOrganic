using BiolifeOrganic.Bll.ViewModels.Order;
using BiolifeOrganic.Bll.ViewModels.UserDiscount;
using BiolifeOrganic.Bll.ViewModels.Wishlist;

namespace BiolifeOrganic.Bll.ViewModels.User;

public class UserViewModel
{
    public string Id { get; set; } = null!;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? ProfileImagePath { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; }

    public int OrdersCount { get; set; }
    public int WishlistCount { get; set; }
    public int UserDiscountsCount { get; set; }
}

public class UserDetailsViewModel
{
    public string Id { get; set; }= null!;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool IsBlocked { get; set; }

    public List<OrderDetailsViewModel> Orders { get; set; } = [];
    public List<WishlistViewModel> Wishlists { get; set; } = [];
    public List<UserDiscountViewModel> Discounts { get; set; } = [];
}

