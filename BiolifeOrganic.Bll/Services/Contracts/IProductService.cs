using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IProductService : ICrudService<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>
{

    IQueryable<Product> GetProductsQuery(
       string? priceFilter = null,
       string? availabilityFilter = null
   );
   Task<ProductViewModel?> GetByIdWithDetailsAsync(int id);
}

