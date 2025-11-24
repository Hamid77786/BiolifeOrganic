using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Wishlist;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services;

public class WishlistManager : CrudManager<Wishlist, WishlistViewModel, CreateWishlistViewModel, UpdateWishlistViewModel>, IWishlistService
{
    public WishlistManager(IWishlistRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }

    public async Task<IEnumerable<WishlistViewModel>> GetUserWishlistAsync(string? userId)
    {
        var items = await Repository.GetAllAsync(
                 predicate: w => w.AppUserId == userId && !w.IsDeleted,
                 include: q => q.Include(w => w.Product)
                               .ThenInclude(p => p!.ProductImages)
                               .Include(w => w.Product)
                               .ThenInclude(p => p!.Category!),
                               
                 orderBy: q => q.OrderByDescending(w => w.CreatedAt),
                 AsNoTracking: true
             );

        return Mapper.Map<IEnumerable<WishlistViewModel>>(items);

    }

    public async Task<int> GetWishlistCountAsync(string? userId)
    {
        var items = await Repository.GetAllAsync(
               predicate: w => w.AppUserId == userId && !w.IsDeleted,
               AsNoTracking: true
           );

        return items.Count;
    }

    public async Task<bool> IsProductInWishlistAsync(int productId, string? userId)
    {
        var item = await Repository.GetAsync(
               predicate: w => w.ProductId == productId && w.AppUserId == userId && !w.IsDeleted,
               AsNoTracking: true
           );

        return item != null;
    }

    public async Task<bool> RemoveFromWishlistAsync(int productId, string? userId)
    {

        var item = await Repository.GetAsync(
            predicate: w => w.ProductId == productId && w.AppUserId == userId && !w.IsDeleted
        );

        if (item == null)
            return false;

        item.IsDeleted = true;
        item.UpdatedAt = DateTime.Now;
        await Repository.UpdateAsync(item);
        return true;
    }

    public async Task<bool> ToggleWishlistAsync(int productId, string? userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("User must be logged in to use wishlist");
        }

        var existing = await Repository.GetAsync(
            predicate: w => w.ProductId == productId && w.AppUserId == userId && !w.IsDeleted
        );

        if (existing != null)
        {
            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;
            await Repository.UpdateAsync(existing);
            return true;
        }
        else
        {
            var newItem = new Wishlist
            {
                ProductId = productId,
                AppUserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await Repository.CreateAsync(newItem);
            return true;
        }
    }
}
