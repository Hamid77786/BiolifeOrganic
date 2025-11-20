using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class WishlistRepository : EfCoreRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}