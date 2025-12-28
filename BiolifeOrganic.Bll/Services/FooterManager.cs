using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels;
using BiolifeOrganic.Bll.ViewModels.WebContact;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services;

public class FooterManager : IFooterService
{
    private readonly ILogoService _logoService;
    private readonly IWebContactService _webContactService;

    public FooterManager(ILogoService logoService, IWebContactService webContactService)
    {
       _logoService = logoService;
        _webContactService = webContactService;
    }

    public async Task<FooterViewModel> GetFooterViewModelAsync()
    {
        var logos = await _logoService.GetAllAsync();

        var webConDefault = (await _webContactService.GetAllAsync(c => c.ContactOption == ContactOption.Web && c.IsDefault)).FirstOrDefault();

        var transConDefault = (await _webContactService.GetAllAsync(c => c.ContactOption == ContactOption.Transport && c.IsDefault)).FirstOrDefault();

        var footerVm = new FooterViewModel
        {
            Logos = logos.ToList(),
            WebContact = webConDefault,
            TransportContact = transConDefault,
        };

        return footerVm;
    }

}
