using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class WebContactRepository : EfCoreRepository<WebContact>, IWebContactRepository
{
    public WebContactRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
