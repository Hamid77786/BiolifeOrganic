using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using System.Net;

namespace BiolifeOrganic.Dll.Reprositories;

public class ContactRepository : EfCoreRepository<Contact>, IContactRepository
{
    public ContactRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Contact>> GetUserAddressesAsync(string userId)
    {
        return await GetAllAsync(
            predicate: a => a.AppUserId == userId && !a.IsDeleted
        ) as List<Contact> ?? new List<Contact>();
    }

    public async Task<Contact?> GetDefaultAddressAsync(string userId)
    {
        return await GetAsync(
            predicate: a => a.AppUserId == userId && !a.IsDeleted
        );
    }

    public async Task<Contact?> GetUserAddressByIdAsync(int contactId, string userId)
    {
        return await GetAsync(
            predicate: a => a.Id == contactId && a.AppUserId == userId && !a.IsDeleted
        );
    }

    public async Task UnsetAllDefaultAddressesAsync(string userId)
    {
        var contacts = await GetAllAsync(
            predicate: a => a.AppUserId == userId && !a.IsDeleted
        );

        await DbContext.SaveChangesAsync();
    }

}
