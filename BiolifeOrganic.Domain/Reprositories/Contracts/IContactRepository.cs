using BiolifeOrganic.Dll.DataContext.Entities;
using System.Net;

namespace BiolifeOrganic.Dll.Reprositories.Contracts;

public interface IContactRepository : IRepository<Contact>
{
    Task<List<Contact>> GetUserAddressesAsync(string userId);
    Task<Contact?> GetDefaultAddressAsync(string userId);
    Task<Contact?> GetUserAddressByIdAsync(int addressId, string userId);
    Task UnsetAllDefaultAddressesAsync(string userId);
}


