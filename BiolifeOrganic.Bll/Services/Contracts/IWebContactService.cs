using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Bll.ViewModels.WebContact;
using BiolifeOrganic.Dll.DataContext.Entities;

namespace BiolifeOrganic.Bll.Services.Contracts;

public interface IWebContactService : ICrudService<WebContact, WebContactViewModel, CreateWebContactViewModel, UpdateWebContactViewModel>
{

}

