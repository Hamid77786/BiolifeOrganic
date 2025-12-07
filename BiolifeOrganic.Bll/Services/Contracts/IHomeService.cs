

using BiolifeOrganic.Bll.ViewModels.Home;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IHomeService
{
    Task<HomeViewModel> GetHomeViewModel(string? userId);
}
