using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Logo;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class LogoManager : CrudManager<Logo, LogoViewModel, CreateLogoViewModel, UpdateLogoViewModel>, ILogoService
{
    public LogoManager(ILogoRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
