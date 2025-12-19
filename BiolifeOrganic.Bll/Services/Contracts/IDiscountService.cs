using BiolifeOrganic.Bll.ViewModels.Discount;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IDiscountService
{
    Task<DiscountValidationResult> ValidateAsync(string code, string? userId, decimal totalAmount);
    Task MarkAsUsedAsync(int discountId, string userId);
    Task AssignWelcomeDiscountAsync(string userId);






}
