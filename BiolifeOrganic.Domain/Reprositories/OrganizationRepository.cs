using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class OrganizationRepository : EfCoreRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
