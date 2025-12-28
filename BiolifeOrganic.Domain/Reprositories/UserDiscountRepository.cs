using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Dll.Reprositories;

public class UserDiscountRepository: EfCoreRepository<UserDiscount>,IUserDiscountRepository
{
    private readonly AppDbContext _context;

    public UserDiscountRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserDiscount?> GetAsync(string userId, int discountId)
       => await _context.UserDiscounts
           .FirstOrDefaultAsync(x => x.AppUserId == userId && x.DiscountId == discountId);

    public async Task<bool> ExistsAsync(string userId, int discountId)
        => await _context.UserDiscounts
            .AnyAsync(x => x.AppUserId == userId && x.DiscountId == discountId);

    public async Task AddAsync(UserDiscount userDiscount)
        => await _context.UserDiscounts.AddAsync(userDiscount);

    public async Task MarkAsUsedAsync(string userId, int discountId)
    {
        var entity = await GetAsync(userId, discountId);
        if (entity != null)
        {
            entity.IsUsed = true;
            entity.UsedAt = DateTime.UtcNow;
        }
    }

}
