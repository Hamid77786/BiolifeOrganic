using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IDiscountService : ICrudService<Discount, DiscountViewModel, CreateDiscountViewModel, UpdateDiscountViewModel>
{
    Task<DiscountValidationResult> ValidateAsync(string code, string? userId, decimal totalAmount);
    Task MarkAsUsedAsync(int discountId, string userId);
    Task AssignWelcomeDiscountAsync(string userId);
    Task AssignLoyaltyDiscountIfEligibleAsync(string userId);
    Task<bool> HasUserDiscountAsync(string userId, string discountCode);







}
