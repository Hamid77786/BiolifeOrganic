using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;
using System.Net;

namespace BiolifeOrganic.Bll.Services;

public class ContactManager :IContactService
{
    private readonly IRepository<Contact> _repository;
    private readonly IMapper _mapper;

    public ContactManager(IRepository<Contact> repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ContactViewModel>> GetUserAddressesAsync(string userId)
    {
        var contacts = await _repository.GetAllAsync(
            predicate: a => a.AppUserId == userId,
            orderBy: q => q.OrderByDescending(a => a.IsDefault).ThenByDescending(a => a.CreatedAt)
        );

        return _mapper.Map<List<ContactViewModel>>(contacts);
    }

    public async Task<ContactViewModel?> GetAddressByIdAsync(int id, string userId)
    {
        var contact = await _repository.GetAsync(
            predicate: a => a.Id == id && a.AppUserId == userId
        );

        return _mapper.Map<ContactViewModel>(contact);
    }

    public async Task<int> CreateAddressAsync(string userId, CreateContactViewModel model)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required");

        var contact = _mapper.Map<Contact>(model);
        contact.AppUserId = userId;

        var contacts = await _repository.GetAllAsync(c => c.AppUserId == userId);

        if (!contacts.Any())
        {
            contact.IsDefault = true;
        }
        else if (contact.IsDefault)
        {
            foreach (var existing in contacts.Where(c => c.IsDefault))
            {
                existing.IsDefault = false;
                await _repository.UpdateAsync(existing);
            }
        }

        await _repository.CreateAsync(contact);
        return contact.Id;
    }

    public async Task<bool> UpdateAddressAsync(int id, string userId, UpdateContactViewModel model)
    {
        var contact = await _repository.GetAsync(a => a.Id == id && a.AppUserId == userId);

        if (contact == null)
            return false;

        _mapper.Map(model, contact);

        if (model.IsDefault && !contact.IsDefault)
        {
            var otherContacts = await _repository.GetAllAsync(
                predicate: a => a.AppUserId == userId && a.Id != id && a.IsDefault
            );

            foreach (var other in otherContacts)
            {
                other.IsDefault = false;
                await _repository.UpdateAsync(other);
            }
        }

        await _repository.UpdateAsync(contact);
        return true;
    }

    public async Task<bool> DeleteAddressAsync(int id, string userId)
    {
        var contact = await _repository.GetAsync(a => a.Id == id && a.AppUserId == userId);

        if (contact == null)
            return false;

        var existDefault = contact.IsDefault;

        await _repository.DeleteAsync(contact);

        if (existDefault)
        {
            var remainingContacts = await _repository.GetAllAsync(
                predicate: a => a.AppUserId == userId,
                orderBy: q => q.OrderByDescending(a => a.CreatedAt)
            );

            var nextDefault = remainingContacts.FirstOrDefault();
            if (nextDefault != null)
            {
                nextDefault.IsDefault = true;
                await _repository.UpdateAsync(nextDefault);
            }
        }

        return true;
    }

    public async Task<ContactViewModel?> GetDefaultAddressAsync(string userId)
    {
        var contact = await _repository.GetAsync(
            predicate: a => a.AppUserId == userId && a.IsDefault
        );

        if (contact == null)
            return null;

        return _mapper.Map<ContactViewModel>(contact);
    }

    public async Task<bool> SetDefaultAddressAsync(int id, string userId)
    {
        var contact = await _repository.GetAsync(a => a.Id == id && a.AppUserId == userId);
        if (contact == null) return false;

        var allContacts = await _repository.GetAllAsync(a => a.AppUserId == userId);
        foreach (var cont in allContacts)
        {
            cont.IsDefault = (cont.Id == id);
            await _repository.UpdateAsync(cont);
        }

        return true;
    }


}

