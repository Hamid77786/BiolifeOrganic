using BiolifeOrganic.Bll.ViewModels.Product;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ISearchService
{
    Task<IEnumerable<ProductViewModel>> GetAllProductWithSearch(int? categoryId, string? productName);
}
