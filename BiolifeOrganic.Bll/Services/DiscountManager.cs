using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services;

public class DiscountManager : IDiscountService
{
    private readonly AppDbContext _context;

    public DiscountManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task MarkAsUsedAsync(int discountId, string userId)
    {
        var existing = await _context.UserDiscounts
            .FirstOrDefaultAsync(x => x.AppUserId == userId && x.DiscountId == discountId);

        if (existing != null)
        {
            existing.IsUsed = true;
            existing.UsedAt = DateTime.UtcNow;
        }
        else
        {
            _context.UserDiscounts.Add(new UserDiscount
            {
                AppUserId = userId,
                DiscountId = discountId,
                IsUsed = true,
                UsedAt = DateTime.UtcNow
            });
        }

        var discount = await _context.Discounts.FindAsync(discountId);
        if (discount != null)
            discount.UsedCount++;

        await _context.SaveChangesAsync();
    }


    private DiscountValidationResult Invalid(string error)
        => new() { IsValid = false, Error = error };

    public async Task<DiscountValidationResult> ValidateAsync(string code, string? userId, decimal totalAmount)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Invalid("No discount code provided");

        var now = DateTime.UtcNow;

        var discount = await _context.Discounts
            .FirstOrDefaultAsync(x =>
                x.Code == code &&
                x.IsActive &&
                x.StartDate <= now &&
                x.EndDate >= now);

        if (discount == null)
            return Invalid("Invalid discount code");

        if (discount.MaxUsageCount.HasValue && discount.UsedCount >= discount.MaxUsageCount)
            return Invalid("Discount limit reached");

        if (!string.IsNullOrEmpty(userId))
        {
            if (discount.OnlyForNewUsers)
            {
                var userDiscount = await _context.UserDiscounts
                    .FirstOrDefaultAsync(x => x.AppUserId == userId && x.DiscountId == discount.Id);

                if (userDiscount != null && userDiscount.IsUsed)
                    return Invalid("Discount already used");

                bool hasOrders = await _context.Orders
                    .AnyAsync(o => o.AppUserId == userId && !o.IsDeleted);

                if (hasOrders)
                    return Invalid("Discount only for new users");

                if (userDiscount == null)
                {
                    _context.UserDiscounts.Add(new UserDiscount
                    {
                        AppUserId = userId,
                        DiscountId = discount.Id,
                        IsUsed = false
                    });

                    await _context.SaveChangesAsync();
                }
            }

            if (discount.OnlyForExistingUsers)
            {
                bool hasOrders = await _context.Orders
                    .AnyAsync(o => o.AppUserId == userId && !o.IsDeleted);

                if (!hasOrders)
                    return Invalid("Discount only for existing users");

            }
        }

        return new DiscountValidationResult
        {
            IsValid = true,
            Percentage = discount.Percentage,
            DiscountId = discount.Id
        };
    }




    
    public async Task AssignWelcomeDiscountAsync(string userId)
    {
        var discount = await _context.Discounts.FirstOrDefaultAsync(x =>
            x.Code == "WELCOME15" &&
            x.IsActive &&
            x.StartDate <= DateTime.UtcNow &&
            x.EndDate >= DateTime.UtcNow);

        if (discount == null)
            return;

        bool alreadyAssigned = await _context.UserDiscounts
            .AnyAsync(x => x.AppUserId == userId && x.DiscountId == discount.Id);

        if (alreadyAssigned)
            return;

        _context.UserDiscounts.Add(new UserDiscount
        {
            AppUserId = userId,
            DiscountId = discount.Id,
            IsUsed = false
        });

        await _context.SaveChangesAsync();
    }

}
