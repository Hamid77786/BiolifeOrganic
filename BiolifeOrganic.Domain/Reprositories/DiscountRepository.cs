using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Dll.Reprositories;

public class DiscountRepository : EfCoreRepository<Discount>, IDiscountRepository
{
    private readonly AppDbContext _context;

    public DiscountRepository(AppDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Discount?> GetValidByCodeAsync(string code, DateTime now)
    {
        return await _context.Discounts.FirstOrDefaultAsync(x =>
            x.Code == code &&
            x.IsActive &&
            x.StartDate <= now &&
            x.EndDate >= now);
    }

    public async Task IncrementUsedCountAsync(int discountId)
    {
        var discount = await _context.Discounts.FindAsync(discountId);
        if (discount != null)
        {
            discount.UsedCount = (discount.UsedCount ?? 0) + 1;
            await _context.SaveChangesAsync();
        }
    }


}

