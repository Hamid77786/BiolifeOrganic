using BiolifeOrganic.Bll.ViewModels.Organization;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IOrganizationService : ICrudService<Organization, OrganizationViewModel, CreateOrganizationViewModel, UpdateOrganizationViewModel>
{
}

