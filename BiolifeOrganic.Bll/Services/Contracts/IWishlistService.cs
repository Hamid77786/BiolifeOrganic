using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IWishlistService : ICrudService<Wishlist, WishlistViewModel, CreateWishlistViewModel, UpdateWishlistViewModel>
{
}

