using BiolifeOrganic.Bll.ViewModels.Contact;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IContactService:ICrudService<Contact,ContactViewModel, CreateContactViewModel, UpdateContactViewModel>
{

}

