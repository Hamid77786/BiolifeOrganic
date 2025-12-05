using BiolifeOrganic.Bll.ViewModels.Basket;
using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.Logo;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Search;
using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Bll.ViewModels.WebContact;

namespace BiolifeOrganic.Bll.ViewModels.Header;

public class HeaderViewModel
{
    public List<CategoryViewModel> Categories { get; set; } = [];
    public List<WebContactViewModel> WebContacts { get; set; } = [];
    public List<LogoViewModel> Logos { get; set; } = [];
    public List<BasketItemViewModel> Items { get; set; } = [];

}
