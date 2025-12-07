using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Dll.Reprositories;

public class WishlistRepository : EfCoreRepository<Wishlist>, IWishlistRepository
{
    private readonly AppDbContext _dbContext;
    public WishlistRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(string userId, int productId)
    {
        var exist = await _dbContext.Wishlists
            .AnyAsync(x => x.AppUserId == userId && x.ProductId == productId);

        if (!exist)
        {
            _dbContext.Wishlists.Add(new Wishlist
            {
                AppUserId = userId,
                ProductId = productId
            });

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<int>> GetUserWishlistIdsAsync(string userId)
    {
        return await _dbContext.Wishlists
            .Where(x => x.AppUserId == userId)
            .Select(x => x.ProductId)
            .ToListAsync();
    }

    public async Task RemoveAsync(string userId, int productId)
    {
        var item = await _dbContext.Wishlists
            .FirstOrDefaultAsync(x => x.AppUserId == userId && x.ProductId == productId);

        if (item != null)
        {
            _dbContext.Wishlists.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}