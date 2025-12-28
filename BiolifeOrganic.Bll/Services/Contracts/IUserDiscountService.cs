using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.UserDiscount;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts
{
    public interface IUserDiscountService : ICrudService<UserDiscount, UserDiscountViewModel, CreateUserDiscountViewModel, UpdateUserDiscountViewModel>
    {
    }
}
