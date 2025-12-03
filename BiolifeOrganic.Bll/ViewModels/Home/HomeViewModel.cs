using BiolifeOrganic.Bll.ViewModels.Category;
using BiolifeOrganic.Bll.ViewModels.Product;
using BiolifeOrganic.Bll.ViewModels.Slider;

namespace BiolifeOrganic.Bll.ViewModels.Home;

public class HomeViewModel
{
    public List<CategoryViewModel> Categories { get; set; } = [];
    public List<ProductViewModel> Products { get; set; } = [];
    public List<SliderViewModel> Sliders { get; set; } = [];



}
