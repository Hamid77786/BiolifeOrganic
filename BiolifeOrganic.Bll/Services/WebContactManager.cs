using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.ViewModels.Slider;
using BiolifeOrganic.Bll.ViewModels.WebContact;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Bll.Services;

public class WebContactManager : CrudManager<WebContact,WebContactViewModel,CreateWebContactViewModel,UpdateWebContactViewModel>,IWebContactService
{


    public WebContactManager(IWebContactRepository respository, IMapper mapper) : base(respository, mapper)
    {
    }
}



  


