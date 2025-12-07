using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IWishlistService : ICrudService<Wishlist, WishlistViewModel, CreateWishlistViewModel, UpdateWishlistViewModel>
{
    Task AddAsync(string userId, int productId);
    Task RemoveAsync(string userId, int productId);
    Task<IEnumerable<WishlistViewModel>> GetUsersWishlistAsync(string? userId);
    Task<IEnumerable<WishlistItemViewModel>> GetUsersDetailWishlistAsync(string? userId);
    Task<bool> RemoveFromWishlistAsync(int productId, string? userId);
    Task<bool> IsProductInWishlistAsync(int productId, string? userId);
    Task<int> GetWishlistCountAsync(string? userId);
    Task<bool> ToggleWishlistAsync(int productId, string? userId);
    Task<List<int>> GetUserWishlistIdsAsync(string userId);


    //Task<WishlistViewModel> GetWishlistByUserId(string userId);
    //void AddToWishlist(CreateWishlistViewModel model);
    //void UpdateWishlist(UpdateWishlistViewModel model);
}


