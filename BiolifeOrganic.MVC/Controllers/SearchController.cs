using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace BiolifeOrganic.MVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
      
        [HttpGet]
        public async Task<IActionResult> Index(string searchText,int categoryId)
        {
            IEnumerable<ProductViewModel> products = await _searchService.GetAllProductWithSearch(categoryId,searchText);

            return View(products);
        }
    }
}
