using AutoMapper;
using BiolifeOrganic.Bll.Constants;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BiolifeOrganic.Bll.Services
{
    public class CategoryManager : CrudManager<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>, ICategoryService
    {
        private readonly FileService _fileService;
        public CategoryManager(ICategoryRepository respository, IMapper mapper,FileService fileService) : base(respository, mapper)
        {
            _fileService = fileService;
        }

       

        public override async Task CreateAsync(CreateCategoryViewModel createViewModel)
        {
            if (createViewModel.ImageFile != null)
            {
                createViewModel.ImageUrl = await _fileService.GenerateFile(createViewModel.ImageFile, FilePathConstants.CategoryImagePath);
            }
            await base.CreateAsync(createViewModel);
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

        public override async Task<bool> UpdateAsync(int id, UpdateCategoryViewModel model)
        {
            if (model.ImageFile != null)
            {
                var oldImageName = model.ImageUrl;

                model.ImageUrl = await _fileService.GenerateFile(
                    model.ImageFile,
                    FilePathConstants.CategoryImagePath
                );

                if (!string.IsNullOrEmpty(oldImageName))
                {
                    var oldFilePath = Path.Combine(FilePathConstants.CategoryImagePath, oldImageName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }

            return await base.UpdateAsync(id, model);
        }
    }
}
