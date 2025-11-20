using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class ContactRepository : EfCoreRepository<Contact>, IContactRepository
{
    public ContactRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
