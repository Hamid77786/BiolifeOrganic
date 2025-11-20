using BiolifeOrganic.Bll.ViewModels.Logo;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface ILogoService : ICrudService<Logo, LogoViewModel, CreateLogoViewModel, UpdateLogoViewModel>
{
}

