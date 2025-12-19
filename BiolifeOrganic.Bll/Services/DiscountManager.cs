using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services;

public class DiscountManager : IDiscountService
{
    private readonly AppDbContext _context;
    private readonly IOrderService _orderService;

    public DiscountManager(AppDbContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    public async Task MarkAsUsedAsync(int discountId, string userId)
    {
        _context.UserDiscounts.Add(new UserDiscount
        {
            AppUserId = userId,
            DiscountId = discountId,
            IsUsed = true,
            UsedAt = DateTime.UtcNow
        });

        var discount = await _context.Discounts.FindAsync(discountId);
        if (discount != null)
            discount.UsedCount++;

        await _context.SaveChangesAsync();
    }

    private DiscountValidationResult Invalid(string error)
        => new() { IsValid = false, Error = error };

    

    public async Task<DiscountValidationResult> ValidateAsync(string code, string? userId, decimal totalAmount)
    {
        var discount = await _context.Discounts
           .FirstOrDefaultAsync(x =>
               x.Code == code &&
               x.IsActive &&
               x.StartDate <= DateTime.UtcNow &&
               x.EndDate >= DateTime.UtcNow);

        if (discount == null)
            return Invalid("Invalid discount code");

        if (discount.MaxUsageCount.HasValue &&
            discount.UsedCount >= discount.MaxUsageCount)
            return Invalid("Discount limit reached");

        if (discount.OnlyForNewUsers && userId != null)
        {
            if (string.IsNullOrEmpty(userId))
                return Invalid("Please login to use this discount");


            if (await _orderService.HasOrdersAsync(userId))
                return Invalid("Discount only for new users");
        }
        if (!string.IsNullOrEmpty(userId))
        {
            bool alreadyUsed = await _context.UserDiscounts
                .AnyAsync(x =>
                    x.AppUserId == userId &&
                    x.DiscountId == discount.Id &&
                    x.IsUsed);

            if (alreadyUsed)
                return Invalid("Discount already used");
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
