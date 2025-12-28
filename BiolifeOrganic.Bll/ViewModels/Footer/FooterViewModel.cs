using BiolifeOrganic.Bll.ViewModels.Logo;
using BiolifeOrganic.Bll.ViewModels.WebContact;

namespace BiolifeOrganic.Bll.ViewModels;

public class FooterViewModel
{
    public List<LogoViewModel> Logos { get; set; } = [];
    public WebContactViewModel? WebContact { get; set; }
    public WebContactViewModel? TransportContact { get; set; }

}
