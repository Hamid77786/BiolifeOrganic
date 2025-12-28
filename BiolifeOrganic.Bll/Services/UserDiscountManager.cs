using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.UserDiscount;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class UserDiscountManager : CrudManager<UserDiscount, UserDiscountViewModel, CreateUserDiscountViewModel, UpdateUserDiscountViewModel>, IUserDiscountService
{
    public UserDiscountManager(IUserDiscountRepository repository, IMapper mapper) : base(repository, mapper)
    {

    }

   
}
