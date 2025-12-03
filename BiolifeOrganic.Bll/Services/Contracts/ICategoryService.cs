using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Dll.DataContext.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ICategoryService : ICrudService<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>
{
    

}

