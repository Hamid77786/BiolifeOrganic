using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class ProductManager : CrudManager<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>, IProductService
{
    public ProductManager(IProductRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}
