using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class ContactManager : CrudManager<Contact, ContactViewModel, CreateContactViewModel, UpdateContactViewModel>, IContactService
{

    public ContactManager(IContactRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}
