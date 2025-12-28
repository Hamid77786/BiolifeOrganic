using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Organization;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class OrganizationManager : CrudManager<Organization, OrganizationViewModel, CreateOrganizationViewModel, UpdateOrganizationViewModel>, IOrganizationService
{
    public OrganizationManager(IOrganizationRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}