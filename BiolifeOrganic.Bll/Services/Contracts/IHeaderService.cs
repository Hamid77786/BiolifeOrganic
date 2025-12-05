using BiolifeOrganic.Bll.ViewModels.Header;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IHeaderService
{
    Task<HeaderViewModel> GetHeaderViewModelAsync();
}
