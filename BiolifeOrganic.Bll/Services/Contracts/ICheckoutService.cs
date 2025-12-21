using BiolifeOrganic.Bll.ViewModels.Basket;
using BiolifeOrganic.Bll.ViewModels.CheckOut;
using BiolifeOrganic.Bll.ViewModels.Discount;
using System.Security.Claims;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ICheckoutService
{
    Task<CheckoutViewModel?> BuildCheckoutViewModelAsync(ClaimsPrincipal user);
    Task<CheckoutResult> ProcessCheckoutAsync(ClaimsPrincipal user, CheckoutViewModel model);

}
