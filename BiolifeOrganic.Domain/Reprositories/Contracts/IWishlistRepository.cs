using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IWishlistRepository : IRepository<Wishlist>
{
    Task AddAsync(string userId, int productId);
    Task RemoveAsync(string userId, int productId);
    Task<List<int>> GetUserWishlistIdsAsync(string userId);
}


