using BiolifeOrganic.Bll.ViewModels.ProductImage;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IProductImageService : ICrudService<ProductImage, ProductImageViewModel, CreateProductImageViewModel, UpdateProductImageViewModel>
{
}

