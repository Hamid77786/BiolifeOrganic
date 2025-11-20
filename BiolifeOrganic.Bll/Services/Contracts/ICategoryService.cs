using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ICategoryService : ICrudService<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>
{

}

