using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class AdminContactRepository : EfCoreRepository<AdminContact>, IAdminContactRepository
{
    public AdminContactRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

}
