using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class PromocodeRepository : EfCoreRepository<Promocode>, IPromocodeRepository
{
    public PromocodeRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
