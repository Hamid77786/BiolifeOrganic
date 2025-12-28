using AutoMapper;
using BiolifeOrganic.Bll.Constants;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiolifeOrganic.Bll.Services
{
    public class CategoryManager : CrudManager<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryManager(ICategoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _categoryRepository = repository;
            _mapper = mapper;
            
        }

        public async Task<CategoryViewModel?> GetCategoryWithProductsAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(
                c => c.Id == id,
                include: q => q.Include(c => c.Products)
            );
            
            if (category == null) return null;

            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

            return categoryViewModel;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories == null) return new List<CategoryViewModel>();

            var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categories);

            return categoryViewModels;
        }
        public async Task<List<SelectListItem>> GetCategorySelectListItemsAsync()
        {
            var categories = await GetAllAsync(predicate: x => !x.IsDeleted);

            return categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
        }



           









      
    }
}
