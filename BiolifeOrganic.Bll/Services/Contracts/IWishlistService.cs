using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IWishlistService : ICrudService<Wishlist, WishlistViewModel, CreateWishlistViewModel, UpdateWishlistViewModel>
{
    Task<IEnumerable<WishlistViewModel>> GetUsersWishlistAsync(string? userId);
    Task<IEnumerable<WishlistItemViewModel>> GetUsersDetailWishlistAsync(string? userId);
    Task<bool> RemoveFromWishlistAsync(int productId, string? userId);
    Task<int> GetWishlistCountAsync(string? userId);
    Task<bool> ToggleWishlistAsync(int productId, string? userId);
    Task<List<int>> GetUserWishlistIdsAsync(string userId);


    
}


