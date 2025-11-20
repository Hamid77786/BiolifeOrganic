using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class LogoRepository : EfCoreRepository<Logo>, ILogoRepository
{
    public LogoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
