using BiolifeOrganic.Bll.ViewModels;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IFooterService
{
    Task<FooterViewModel> GetFooterViewModelAsync();
}
