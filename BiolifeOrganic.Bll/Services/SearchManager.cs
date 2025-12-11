using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class SearchManager : ISearchService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public SearchManager(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProductWithSearch(int? categoryId, string? productName)
    {

        var datas = await _productRepository.GetAllAsync(p =>
        (categoryId == null || p.CategoryId == categoryId) &&
        (string.IsNullOrEmpty(productName) || p.Name.Contains(productName)));


        return _mapper.Map<IEnumerable<ProductViewModel>>(datas);

    }
}
