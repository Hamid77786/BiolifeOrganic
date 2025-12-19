using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IContactService
{
    Task<List<ContactViewModel>> GetUserAddressesAsync(string userId);
    Task<ContactViewModel?> GetAddressByIdAsync(int id, string userId);
    Task<int> CreateAddressAsync(string userId, CreateContactViewModel model);
    Task<bool> UpdateAddressAsync(int id, string userId, UpdateContactViewModel model);
    Task<bool> DeleteAddressAsync(int id, string userId);
    Task<ContactViewModel?> GetDefaultAddressAsync(string userId);
    Task<bool> SetDefaultAddressAsync(int id, string userId);
}

